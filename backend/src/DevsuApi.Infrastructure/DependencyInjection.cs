using DevsuApi.Domain.Interfaces;
using DevsuApi.Domain.Repositories;
using DevsuApi.Domain.Services;
using DevsuApi.Infrastructure.Persistence;
using DevsuApi.Infrastructure.Repositories;
using DevsuApi.Infrastructure.Services;
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

        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<ITransferRepository, TransferRepository>();

        services.AddScoped<IPdfGeneratorService, PdfGeneratorService>();

        return services;
    }
}
