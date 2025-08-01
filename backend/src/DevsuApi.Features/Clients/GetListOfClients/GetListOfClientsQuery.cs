using DevsuApi.Features.Clients.Shared;
using DevsuApi.Domain.Shared;
using MediatR;

namespace DevsuApi.Features.Clients.GetListOfClients;

public sealed class GetListOfClientsQuery : IRequest<Result<List<ClientResponse>>>
{
    // No parameters needed for this Query
    // Later we can implement a pagination or filtering mechanism if required
}
