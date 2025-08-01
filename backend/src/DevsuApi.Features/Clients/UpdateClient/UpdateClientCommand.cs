using DevsuApi.Domain.Enums;
using DevsuApi.Features.Clients.Shared;
using DevsuApi.Domain.Shared;
using MediatR;

namespace DevsuApi.Features.Clients.UpdateClient;

public sealed class UpdateClientCommand : BaseClientCommand, IRequest<Result<ClientResponse>>
{
    public Guid Id { get; set; }
    public Status Status { get; set; }
}
