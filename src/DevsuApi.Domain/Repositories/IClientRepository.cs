using DevsuApi.Domain.Entities;

namespace DevsuApi.Domain.Repositories;

public interface IClientRepository
{
    void Add(Client client);
    IQueryable<Client?> GetById(Guid id);
    Task<Client?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    IQueryable<Client> GetAll();
    void Update(Client client);
    void Remove(Client client);
}
