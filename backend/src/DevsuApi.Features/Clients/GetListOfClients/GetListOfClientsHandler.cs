using DevsuApi.Features.Clients.Shared;
using DevsuApi.Domain.Shared;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using DevsuApi.Domain.Repositories;

namespace DevsuApi.Features.Clients.GetListOfClients;

public sealed class GetListOfClientsHandler(IClientRepository clientRepository) : IRequestHandler<GetListOfClientsQuery, Result<List<ClientResponse>>>
{
    public async Task<Result<List<ClientResponse>>> Handle(GetListOfClientsQuery request, CancellationToken cancellationToken)
    {
        var clients = await clientRepository.GetAll()
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
