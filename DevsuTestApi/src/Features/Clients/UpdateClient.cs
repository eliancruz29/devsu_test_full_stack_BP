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

public static class UpdateClient
{
    public class Command : IRequest<Result<ClientResponse>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Identification { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Status Status { get; set; }
    }

    public class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.DateOfBirth).NotEmpty().NotEqual(default(DateTime))
                .WithMessage("Date of Birth must be a valid date.");
            RuleFor(c => c.Identification).NotEmpty().Matches(@"^\d{3}-\d{7}-\d{1}$|^\d{11}$")
                .WithMessage("Identification must be a valid Dominican Republic ID format (###-#######-# or 11 digits).");
            RuleFor(c => c.Address).NotEmpty();
            RuleFor(c => c.PhoneNumber).NotEmpty().Matches(@"^\+?[1-9]\d{1,14}$")
                .WithMessage("Phone number must be a valid international format.");
            RuleFor(c => c.Password).NotEmpty().MinimumLength(6)
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
                    "UpdateClient.Null",
                    "The client with the specified ID was not found"));
            }

            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
            {
                return Result.Failure<ClientResponse>(new Error(
                    "UpdateClient.Validation",
                    validationResult.ToString()));
            }

            client.Update(
                request.Name,
                request.Gender,
                request.DateOfBirth,
                request.Identification,
                request.Address,
                request.PhoneNumber,
                request.Password,
                request.Status);

            dbContext.Update(client);

            await dbContext.SaveChangesAsync(cancellationToken);

            return client.Adapt<ClientResponse>();
        }
    }
}

public class UpdateClientEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("api/clientes/{id:guid}", async (Guid id, [FromBody] UpdateClientRequest request, ISender sender) =>
        {
            if (id != request.Id)
            {
                return Results.BadRequest(new { Message = "Id in URL does not match the request body." });
            }

            var command = request.Adapt<UpdateClient.Command>();
            command.Id = id;

            var result = await sender.Send(command);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }

            return Results.NoContent();
        })
        .WithName("UpdateClient")
        .WithTags("Clients")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status400BadRequest);
    }
}
