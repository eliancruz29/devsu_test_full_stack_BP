using DevsuApi.Domain.Entities;
using DevsuApi.Domain.Repositories;
using DevsuApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevsuApi.Infrastructure.Repositories;

internal class TransferRepository : ITransferRepository
{
    private readonly DevsuApiDbContext _context;

    public TransferRepository(DevsuApiDbContext context)
    {
        _context = context;
    }

    public void Add(Transfer transfer)
    {
        _context.Transfers.Add(transfer);
    }

    public IQueryable<Transfer?> GetById(Guid id)
    {
        return _context.Transfers.AsNoTracking().Where(c => c.Id == id);
    }

    public Task<Transfer?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _context.Transfers.SingleOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public IQueryable<Transfer> GetAll()
    {
        return _context.Transfers.AsNoTracking(); // Use AsNoTracking for read-only queries;
    }

    public void Update(Transfer transfer)
    {
        _context.Transfers.Update(transfer);
    }

    public void Remove(Transfer transfer)
    {
        _context.Transfers.Remove(transfer);
    }
}
