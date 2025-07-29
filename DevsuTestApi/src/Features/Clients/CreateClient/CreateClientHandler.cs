using DevsuTestApi.Database;
using DevsuTestApi.Entities;
using DevsuTestApi.Shared;
using FluentValidation;
using MediatR;

namespace DevsuTestApi.Features.Clients.CreateClient;

public sealed class CreateClientHandler(ApplicationDbContext dbContext, IValidator<CreateClientCommand> validator) : IRequestHandler<CreateClientCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(request);
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

        dbContext.Add(client);

        await dbContext.SaveChangesAsync(cancellationToken);

        return client.Id;
    }
}
