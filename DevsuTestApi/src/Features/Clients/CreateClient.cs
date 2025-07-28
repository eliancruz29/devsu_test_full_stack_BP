using Carter;
using DevsuTestApi.Contracts;
using DevsuTestApi.Database;
using DevsuTestApi.Shared;
using FluentValidation;
using MediatR;
using Mapster;
using DevsuTestApi.Enums;
using DevsuTestApi.Entities;

namespace DevsuTestApi.Features.Clients;

public static class CreateClient
{
    public class Command : IRequest<Result<Guid>>
    {
        public string Name { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Identification { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
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

    internal sealed class Handler : IRequestHandler<Command, Result<Guid>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IValidator<Command> _validator;

        public Handler(ApplicationDbContext dbContext, IValidator<Command> validator)
        {
            _dbContext = dbContext;
            _validator = validator;
        }

        public async Task<Result<Guid>> Handle(Command request, CancellationToken cancellationToken)
        {
            var validationResult = _validator.Validate(request);
            if (!validationResult.IsValid)
            {
                return Result.Failure<Guid>(new Error(
                    "CreateClient.Validation",
                    validationResult.ToString()));
            }

            var client = Client.Create(
                request.Name,
                request.Gender,
                request.DateOfBirth,
                request.Identification,
                request.Address,
                request.PhoneNumber,
                request.Password);

            _dbContext.Add(client);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return client.Id;
        }
    }
}

public class CreateClientEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/clientes", async (CreateClientRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateClient.Command>();

            var result = await sender.Send(command);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }

            return Results.Ok(result.Value);
        });
    }
}
