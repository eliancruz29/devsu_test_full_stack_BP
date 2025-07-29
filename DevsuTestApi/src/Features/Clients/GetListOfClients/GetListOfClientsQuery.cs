using DevsuTestApi.Features.Clients.Shared;
using DevsuTestApi.Shared;
using MediatR;

namespace DevsuTestApi.Features.Clients.GetListOfClients;

public sealed class GetListOfClientsQuery : IRequest<Result<List<ClientResponse>>>
{
    // No parameters needed for this Query
    // Later we can implement a pagination or filtering mechanism if required
}

