using InoveFk.Core.Notification;
using InoveFk.Core.Services;
using InoveFk.Core.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace InoveFk.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCoreServiceDependency(this IServiceCollection services)
    {
        services.AddScoped<NotificationContext>();
        services.AddSingleton<IRabbitMQPublisher, RabbitMQPublisher>();

        return services;
    }
}
