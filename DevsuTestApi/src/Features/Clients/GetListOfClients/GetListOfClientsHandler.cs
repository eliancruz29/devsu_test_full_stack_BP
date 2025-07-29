using DevsuTestApi.Database;
using DevsuTestApi.Features.Clients.Shared;
using DevsuTestApi.Shared;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevsuTestApi.Features.Clients.GetListOfClients;

public sealed class GetListOfClientsHandler(ApplicationDbContext dbContext) : IRequestHandler<GetListOfClientsQuery, Result<List<ClientResponse>>>
{
    public async Task<Result<List<ClientResponse>>> Handle(GetListOfClientsQuery request, CancellationToken cancellationToken)
    {
        var clients = await dbContext
            .Clients
            .AsNoTracking() // Use AsNoTracking for read-only queries
            .ProjectToType<ClientResponse>()
            .ToListAsync(cancellationToken);

        if (clients is null)
        {
            return Result.Failure<List<ClientResponse>>(new Error(
                "GetListOfClients.Null",
                "The list clients was not found"));
        }

        return Result.Success(clients);
    }
}
