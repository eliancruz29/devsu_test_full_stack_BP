using Microsoft.EntityFrameworkCore;
using DevsuApi.Domain.Entities;

namespace DevsuApi.Domain.Interfaces;

public interface IDevsuApiDbContext
{
    DbSet<Client> Clients { get; set; }

    DbSet<Account> Accounts { get; set; }

    DbSet<Transfer> Transfers { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
