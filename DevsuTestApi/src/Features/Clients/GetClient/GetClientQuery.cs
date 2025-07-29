using DevsuTestApi.Features.Clients.Shared;
using DevsuTestApi.Shared;
using MediatR;

namespace DevsuTestApi.Features.Clients.GetClient;

public sealed class GetClientQuery : IRequest<Result<ClientResponse>>
{
    public Guid Id { get; set; }
}
