using RabbitMQ.Client;
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
    exchange: "header-excange-example",
    type: ExchangeType.Headers);

for (int i = 0; i < 100; i++)
{
    await Task.Delay(200);

    byte[] message = Encoding.UTF8.GetBytes($"Merhaba, {i}");

    Console.Write("Lütfen header value'sunu giriniz: ");
    string value = Console.ReadLine()!;

    IBasicProperties basicProperties = channel.CreateBasicProperties();

    basicProperties.Headers = new Dictionary<string, object>
    {
        ["name"] = value!
    };

    channel.BasicPublish(
        exchange: "header-exchange-example",
        routingKey: string.Empty,
        body: message,
        basicProperties: basicProperties);
}

Console.ReadLine();