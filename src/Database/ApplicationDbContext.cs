using Microsoft.EntityFrameworkCore;
using DevsuTestApi.Entities;
using DevsuTestApi.Enums;
using DevsuTestApi.Extensions;

namespace DevsuTestApi.Database;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
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
}
