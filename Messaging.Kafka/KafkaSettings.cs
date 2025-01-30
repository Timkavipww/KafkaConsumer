namespace Messaging.Kafka;

public class KafkaSettings
{
    public string BoostrapServers { get; set; }
    public string Topic { get; set; }         
    public string GroupId { get; set; }
}
