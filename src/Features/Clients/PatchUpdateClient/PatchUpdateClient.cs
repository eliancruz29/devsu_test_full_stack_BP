using DevsuTestApi.Database;
using DevsuTestApi.Features.Clients.Shared;
using DevsuTestApi.Shared;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevsuTestApi.Features.Clients.PatchUpdateClient;

public sealed class PatchUpdateClientHandler(ApplicationDbContext dbContext, IValidator<PatchUpdateClientCommand> validator) : IRequestHandler<PatchUpdateClientCommand, Result<ClientResponse>>
{
    public async Task<Result<ClientResponse>> Handle(PatchUpdateClientCommand request, CancellationToken cancellationToken)
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
