using DevsuApi.Domain.Entities;
using DevsuApi.Domain.Repositories;
using DevsuApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevsuApi.Infrastructure.Repositories;

internal class AccountRepository : IAccountRepository
{
    private readonly DevsuApiDbContext _context;

    public AccountRepository(DevsuApiDbContext context)
    {
        _context = context;
    }

    public void Add(Account account)
    {
        _context.Accounts.Add(account);
    }

    public IQueryable<Account?> GetById(Guid id)
    {
        return _context.Accounts.AsNoTracking().Where(c => c.Id == id);
    }

    public Task<Account?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _context.Accounts.SingleOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<Account?> GetByIdWithTransfersAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Accounts
            .Include(a => a.Transfers)
            .AsSplitQuery()
            .AsNoTracking()
            .SingleOrDefaultAsync(a => a.Id == id, cancellationToken);
    }

    public IQueryable<Account> GetAll()
    {
        return _context.Accounts.AsNoTracking(); // Use AsNoTracking for read-only queries;
    }

    public void Update(Account account)
    {
        _context.Accounts.Update(account);
    }

    public void Remove(Account account)
    {
        _context.Accounts.Remove(account);
    }
}
