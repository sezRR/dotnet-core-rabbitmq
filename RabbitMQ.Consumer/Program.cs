// Creating Connection
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

ConnectionFactory factory = new()
{
    Uri = new("amqps://hmgfrnrc:Nh3X_0X0uRgt1CoXZ1-lFk3FGaLQylhC@shark.rmq.cloudamqp.com/hmgfrnrc")
};

// Activating Connection and Creating Channel
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "direct-exchange-example", type: ExchangeType.Direct);

string queueName = channel.QueueDeclare().QueueName;

channel.QueueBind(
    queue: queueName,
    exchange: "direct-exchange-example",
    routingKey: "direct-queue-example");

EventingBasicConsumer consumer = new(channel);

channel.BasicConsume(
    queue: queueName,
    autoAck: true,
    consumer: consumer);

consumer.Received += (sender, e) =>
{
    string message = Encoding.UTF8.GetString(e.Body.Span);
    Console.WriteLine(message);
};

#region Advanced Queue
//// Configuring Consumer
//channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

//// Creating Queue
//channel.QueueDeclare(queue: "first-queue", exclusive: false, durable: true);

//// Read Message(s) from Queue
//EventingBasicConsumer consumer = new(channel);
//channel.BasicConsume(queue: "first-queue", autoAck: false, consumer: consumer);

//consumer.Received += (sender, e) =>
//{
//    // In this block, we are processing the message coming from the Producer
//    // e.Body.Span or e.Body.ToArray(): These methods are provides the byte data from queue to us.

//    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));

//    channel.BasicAck(deliveryTag: e.DeliveryTag, multiple: false); // Message Acknowledgement
//};

#endregion

Console.ReadLine();
