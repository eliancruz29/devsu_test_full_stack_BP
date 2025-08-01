using DevsuApi.Domain.Entities;

namespace DevsuApi.Domain.Repositories;

public interface IAccountRepository
{
    void Add(Account account);
    IQueryable<Account?> GetById(Guid id);
    Task<Account?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Account?> GetByIdWithTransfersAsync(Guid id, CancellationToken cancellationToken);
    IQueryable<Account> GetAll();
    void Update(Account account);
    void Remove(Account account);
}
