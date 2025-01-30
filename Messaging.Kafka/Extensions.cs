using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Messaging.Kafka;

public static class Extensions
{
    public static IServiceCollection AddConsumer<TMessage, THandler>(this IServiceCollection services, IConfigurationSection configurationSection)
    where THandler :class, IMessageHandler<TMessage>
    {
        services.Configure<KafkaSettings>(configurationSection);
        services.AddHostedService<KafkaConsumer<TMessage>>();
        services.AddSingleton<IMessageHandler<TMessage>, THandler>();
         
        return services;
    }
}
