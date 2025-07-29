using DevsuTestApi.Database;
using DevsuTestApi.Features.Clients.Shared;
using DevsuTestApi.Shared;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevsuTestApi.Features.Clients.GetClient;

public sealed class GetClientHandler(ApplicationDbContext dbContext) : IRequestHandler<GetClientQuery, Result<ClientResponse>>
{
    public async Task<Result<ClientResponse>> Handle(GetClientQuery request, CancellationToken cancellationToken)
    {
        var clientResponse = await dbContext
            .Clients
            .AsNoTracking() // Use AsNoTracking for read-only queries
            .Where(client => client.Id == request.Id)
            .ProjectToType<ClientResponse>()
            .FirstOrDefaultAsync(cancellationToken);

        if (clientResponse is null)
        {
            return Result.Failure<ClientResponse>(new Error(
                "GetClient.Null",
                "The client with the specified ID was not found"));
        }

        return clientResponse;
    }
}
