using FluentValidation;
using MediatR.NotificationPublishers;
using Microsoft.Extensions.DependencyInjection;

namespace DevsuApi.Features;

public static class DependencyInjection
{
    public static IServiceCollection AddFeatures(this IServiceCollection services)
    {        
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblyContaining<FeaturesAssemblyReference>();

            config.NotificationPublisher = new TaskWhenAllPublisher();
        });

        services.AddValidatorsFromAssembly(FeaturesAssemblyReference.Assembly);

        return services;
    }
}
