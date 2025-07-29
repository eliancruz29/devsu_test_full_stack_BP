using DevsuApi.Features.Clients.Shared;
using DevsuApi.Domain.Shared;
using MediatR;

namespace DevsuApi.Features.Clients.GetClient;

public sealed class GetClientQuery : IRequest<Result<ClientResponse>>
{
    public Guid Id { get; set; }
}
