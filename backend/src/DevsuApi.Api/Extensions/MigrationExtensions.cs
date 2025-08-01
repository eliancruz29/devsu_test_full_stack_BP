using DevsuApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevsuApi.Api.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var dbContext = scope.ServiceProvider.GetRequiredService<DevsuApiDbContext>();

        dbContext.Database.Migrate();
    }
}