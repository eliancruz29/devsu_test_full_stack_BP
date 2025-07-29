using System.Reflection;
using Carter;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DevsuApi.Features;

public static class DependencyInjection
{
    public static IServiceCollection AddFeatures(this IServiceCollection services, Assembly assembly)
    {
        services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));

        services.AddCarter();

        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}
