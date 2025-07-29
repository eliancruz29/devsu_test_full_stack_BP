using DevsuTestApi.Enums;
using DevsuTestApi.Features.Clients.Shared;
using DevsuTestApi.Shared;
using MediatR;

namespace DevsuTestApi.Features.Clients.UpdateClient;

public sealed class UpdateClientCommand : BaseClientCommand, IRequest<Result<ClientResponse>>
{
    public Guid Id { get; set; }
    public Status Status { get; set; }
}
