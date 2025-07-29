using DevsuApi.Features.Clients.Shared;
using DevsuApi.Domain.Shared;
using MediatR;

namespace DevsuApi.Features.Clients.CreateClient;

public sealed class CreateClientCommand : BaseClientCommand, IRequest<Result<Guid>>
{
}
