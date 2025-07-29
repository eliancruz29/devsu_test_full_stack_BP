using DevsuApi.Domain.Entities;

namespace DevsuApi.Domain.Repositories;

public interface ITransferRepository
{
    IQueryable<Transfer?> GetById(Guid id);
    Task<Transfer?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    IQueryable<Transfer> GetAll();
    void Update(Transfer transfer);
    void Remove(Transfer transfer);
}
