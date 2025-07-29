using Carter;
using DevsuTestApi.Contracts.Clients;
using DevsuTestApi.Database;
using DevsuTestApi.Enums;
using DevsuTestApi.Shared;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevsuTestApi.Features.Clients;

public static class PatchUpdateClient
{
    public class Command : IRequest<Result<ClientResponse>>
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public Gender? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Identification { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; }
        public Status? Status { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(c => c.Identification)
                .Matches(@"^\d{3}-\d{7}-\d{1}$|^\d{11}$")
                .When(c => !string.IsNullOrWhiteSpace(c.Identification))
                .WithMessage("Identification must be a valid Dominican Republic ID format (###-#######-# or 11 digits).");
            RuleFor(c => c.PhoneNumber)
                .Matches(@"^\+?[1-9]\d{1,14}$")
                .When(c => !string.IsNullOrWhiteSpace(c.PhoneNumber))
                .WithMessage("Phone number must be a valid international format.");
            RuleFor(c => c.Password)
                .MinimumLength(6)
                .When(c => !string.IsNullOrWhiteSpace(c.Password))
                .WithMessage("Password must be at least 6 characters long.");
        }
    }

    internal sealed class Handler(ApplicationDbContext dbContext, IValidator<Command> validator) : IRequestHandler<Command, Result<ClientResponse>>
    {
        public async Task<Result<ClientResponse>> Handle(Command request, CancellationToken cancellationToken)
        {
            var client = await dbContext
                .Clients
                .Where(client => client.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (client is null)
            {
                return Result.Failure<ClientResponse>(new Error(
                    "PatchUpdateClient.Null",
                    "The client with the specified ID was not found"));
            }

            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
            {
                return Result.Failure<ClientResponse>(new Error(
                    "PatchUpdateClient.Validation",
                    validationResult.ToString()));
            }

            client.Update(
                request.Name ?? client.Name,
                request.Gender ?? client.Gender,
                request.DateOfBirth ?? client.DateOfBirth,
                request.Identification ?? client.Identification,
                request.Address ?? client.Address,
                request.PhoneNumber ?? client.PhoneNumber,
                request.Password ?? client.Password,
                request.Status ?? client.Status
            );

            dbContext.Update(client);

            await dbContext.SaveChangesAsync(cancellationToken);

            return client.Adapt<ClientResponse>();
        }
    }
}

public class PatchUpdateClientEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPatch("api/clientes/{id:guid}", async (Guid id, [FromBody] PatchUpdateClientRequest request, ISender sender) =>
        {
            if (id != request.Id)
            {
                return Results.BadRequest(new { Message = "Id in URL does not match the request body." });
            }

            var command = request.Adapt<PatchUpdateClient.Command>();
            command.Id = id;

            var result = await sender.Send(command);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }

            return Results.NoContent();
        })
        .WithName("PatchUpdateClient")
        .WithTags("Clients")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status400BadRequest);
    }
}
