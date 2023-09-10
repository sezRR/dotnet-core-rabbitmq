using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

// Creating Connection
ConnectionFactory factory = new()
{
    Uri = new("amqps://hmgfrnrc:Nh3X_0X0uRgt1CoXZ1-lFk3FGaLQylhC@shark.rmq.cloudamqp.com/hmgfrnrc")
};

// Activating Connection and Creating Channel
using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.ExchangeDeclare(
    exchange: "header-exchange-example",
    type: ExchangeType.Headers);

Console.Write("Lütfen header value'sunu giriniz: ");
string value = Console.ReadLine()!;

string queueName = channel.QueueDeclare().QueueName;

channel.QueueBind(
    queue: queueName,
    exchange: "header-exchange-example",
    routingKey: string.Empty,
    new Dictionary<string, object>
    {
        ["x-match"] = "all",
        ["name"] = value
    });

EventingBasicConsumer consumer = new(channel);

channel.BasicConsume(
    queue: queueName,
    autoAck: true,
    consumer: consumer
    );

consumer.Received += (sender, args) =>
{
    string message = Encoding.UTF8.GetString(args.Body.Span);

    Console.WriteLine(message);
};

Console.ReadLine();