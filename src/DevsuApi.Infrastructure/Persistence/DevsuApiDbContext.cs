using Microsoft.EntityFrameworkCore;
using DevsuApi.Domain.Entities;
using DevsuApi.Domain.Enums;
using DevsuApi.Infrastructure.Extensions;
using DevsuApi.Domain.Interfaces;
using DevsuApi.SharedKernel;
using MediatR;

namespace DevsuApi.Infrastructure.Persistence;

public class DevsuApiDbContext : DbContext, IDevsuApiDbContext, IUnitOfWork
{
    private readonly IPublisher _publisher;

    public DevsuApiDbContext(DbContextOptions<DevsuApiDbContext> options, IPublisher publisher)
        : base(options)
    {
        _publisher = publisher;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Client
        modelBuilder
            .Entity<Client>()
            .Property(p => p.Gender)
            .HasConversion(new EnumMemberConverter<Gender>());

        modelBuilder
            .Entity<Client>()
            .Property(p => p.Status)
            .HasConversion(new EnumMemberConverter<Status>());
        #endregion

        #region Account
        modelBuilder
            .Entity<Account>()
            .Property(p => p.Type)
            .HasConversion(new EnumMemberConverter<AccountTypes>());
        modelBuilder
            .Entity<Account>()
            .Property(p => p.Status)
            .HasConversion(new EnumMemberConverter<Status>());
        #endregion

        #region Transfer
        modelBuilder
            .Entity<Transfer>()
            .Property(p => p.Type)
            .HasConversion(new EnumMemberConverter<TransferTypes>());
        #endregion
    }

    public DbSet<Client> Clients { get; set; }

    public DbSet<Account> Accounts { get; set; }

    public DbSet<Transfer> Transfers { get; set; }    

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var domainEvents = ChangeTracker.Entries<BaseEntity>()
            .Select(e => e.Entity)
            .Where(e => e.GetDomainEvents().Any())
            .SelectMany(e => e.GetDomainEvents());

        var result = await base.SaveChangesAsync(cancellationToken);
        
        foreach (var domainEvent in domainEvents)
        {
            await _publisher.Publish(domainEvent, cancellationToken);
        }

        return result;
    }
}
