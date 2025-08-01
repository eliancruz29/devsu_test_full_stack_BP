using MediatR;

namespace DevsuApi.Features.Clients.DeleteClient;

public sealed class DeleteClientQuery : IRequest
{
    public Guid Id { get; set; }
}
