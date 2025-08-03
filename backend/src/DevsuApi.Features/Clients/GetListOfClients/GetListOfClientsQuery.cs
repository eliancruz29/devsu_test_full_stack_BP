using DevsuApi.Features.Clients.Shared;
using DevsuApi.Domain.Shared;
using MediatR;

namespace DevsuApi.Features.Clients.GetListOfClients;

public sealed class GetListOfClientsQuery : IRequest<Result<List<ClientResponse>>>
{
    public string? SearchByName { get; set; }
}
