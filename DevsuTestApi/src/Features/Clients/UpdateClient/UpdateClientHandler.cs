using DevsuTestApi.Database;
using DevsuTestApi.Features.Clients.Shared;
using DevsuTestApi.Shared;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevsuTestApi.Features.Clients.UpdateClient;

public sealed class UpdateClientHandler(ApplicationDbContext dbContext, IValidator<UpdateClientCommand> validator) : IRequestHandler<UpdateClientCommand, Result<ClientResponse>>
{
    public async Task<Result<ClientResponse>> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
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
