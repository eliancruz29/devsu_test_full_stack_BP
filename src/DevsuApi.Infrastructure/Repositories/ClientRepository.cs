using DevsuApi.Domain.Entities;
using DevsuApi.Domain.Repositories;
using DevsuApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevsuApi.Infrastructure.Repositories;

internal class ClientRepository : IClientRepository
{
    private readonly DevsuApiDbContext _context;

    public ClientRepository(DevsuApiDbContext context)
    {
        _context = context;
    }

    public void Add(Client client)
    {
        _context.Clients.Add(client);
    }

    public IQueryable<Client?> GetById(Guid id)
    {
        return _context.Clients.AsNoTracking().Where(c => c.Id == id);
    }

    public Task<Client?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _context.Clients.SingleOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public IQueryable<Client> GetAll()
    {
        return _context.Clients.AsNoTracking(); // Use AsNoTracking for read-only queries;
    }

    public void Update(Client client)
    {
        _context.Clients.Update(client);
    }

    public void Remove(Client client)
    {
        _context.Clients.Remove(client);
    }
}
