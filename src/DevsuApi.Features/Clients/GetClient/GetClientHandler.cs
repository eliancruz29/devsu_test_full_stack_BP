using DevsuApi.Features.Clients.Shared;
using DevsuApi.Domain.Shared;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;
using DevsuApi.Domain.Repositories;

namespace DevsuApi.Features.Clients.GetClient;

public sealed class GetClientHandler(IClientRepository clientRepository) : IRequestHandler<GetClientQuery, Result<ClientResponse>>
{
    public async Task<Result<ClientResponse>> Handle(GetClientQuery request, CancellationToken cancellationToken)
    {
        ClientResponse? client = await clientRepository.GetById(request.Id)
            .ProjectToType<ClientResponse>()
            .SingleOrDefaultAsync(cancellationToken);

        if (client is null)
        {
            return Result.Failure<ClientResponse>(new Error(
                "GetClient.Null",
                "The client with the specified ID was not found"));
        }

        return client;
    }
}
