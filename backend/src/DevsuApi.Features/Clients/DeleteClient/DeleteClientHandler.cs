using DevsuApi.Domain.Entities;
using DevsuApi.Domain.Exceptions.Clients;
using DevsuApi.Domain.Interfaces;
using DevsuApi.Domain.Repositories;
using MediatR;

namespace DevsuApi.Features.Clients.DeleteClient;

public sealed class DeleteClientHandler(
    IClientRepository clientRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteClientQuery>
{
    public async Task Handle(DeleteClientQuery request, CancellationToken cancellationToken)
    {
        Client? existantClient = await clientRepository.GetByIdAsync(request.Id, cancellationToken);

        if (existantClient is null)
        {
            throw new ClientNotFoundException(request.Id);
        }

        clientRepository.Remove(existantClient);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
