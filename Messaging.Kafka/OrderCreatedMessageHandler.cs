
using Microsoft.Extensions.Logging;

namespace Messaging.Kafka;

public class OrderCreatedMessageHandler(ILogger<OrderCreatedMessageHandler> _logger) : IMessageHandler<OrderCreated>
{
    public Task HandleAsync(OrderCreated message, CancellationToken cts)
    {
        _logger.LogInformation($"Заказ создан. {message.Id} в {message.createdAt.Date} с именем {message.Name} ");
        return Task.CompletedTask;
    }

}
