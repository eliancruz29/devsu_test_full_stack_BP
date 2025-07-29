using DevsuTestApi.Features.Clients.Shared;
using DevsuTestApi.Shared;
using MediatR;

namespace DevsuTestApi.Features.Clients.CreateClient;

public sealed class CreateClientCommand : BaseClientCommand, IRequest<Result<Guid>>
{
}
