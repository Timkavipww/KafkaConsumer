namespace Messaging.Kafka;

public class OrderCreated
{
    public string Id { get; set; }
    public string Name { get; set; }
    public DateTime createdAt { get; set; }
}
