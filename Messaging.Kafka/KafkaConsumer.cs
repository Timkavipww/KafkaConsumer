using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Messaging.Kafka;

public class KafkaConsumer<TMessage> : BackgroundService
{
    private readonly string _topic;
    private readonly IConsumer<string, TMessage> _consumer;
    private readonly IMessageHandler<TMessage> _messageHandler;

    public KafkaConsumer(IOptions<KafkaSettings> kafkaSettings, IMessageHandler<TMessage> messageHandler)
    {
        var config = new ConsumerConfig{
            BootstrapServers = kafkaSettings.Value.BoostrapServers,
            GroupId = kafkaSettings.Value.GroupId
        };
        
        _topic = kafkaSettings.Value.Topic;

        _consumer = new ConsumerBuilder<string, TMessage>(config)
            .SetValueDeserializer(new KafkaValueDeserializer<TMessage>())
            .Build();
        _messageHandler = messageHandler;

    }

    public override Task StopAsync(CancellationToken cancellationToken)
    {
        _consumer.Close();
        return base.StopAsync(cancellationToken);
    }


    protected override Task ExecuteAsync(CancellationToken cts)
    {
        return Task.Run(() => ConsumeAsync(cts));
    }

    private async Task? ConsumeAsync(CancellationToken cts)
    {
        _consumer.Subscribe(_topic);
        try
        {
            while(!cts.IsCancellationRequested)
            {
                var result = _consumer.Consume(cts);
                await _messageHandler.HandleAsync(result.Message.Value, cts);

            }
        }
        catch (Exception e)
        {
            
            throw e;
        }

    }

}
