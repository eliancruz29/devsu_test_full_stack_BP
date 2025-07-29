using DevsuApi.Domain.Interfaces;
using DevsuApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevsuApi.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<DevsuApiDbContext>(o =>
            o.UseSqlServer(config.GetConnectionString("Database")));

        services.AddScoped<IUnitOfWork>(sp =>
            sp.GetRequiredService<DevsuApiDbContext>());
        return services;
    }
}
