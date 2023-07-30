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

// Creating Queue
channel.QueueDeclare(queue: "first-queue", exclusive: false, durable: true);

// Sending Message to Queue

//byte[] message = Encoding.UTF8.GetBytes("Hello, World!");
//channel.BasicPublish(exchange: "", routingKey: "first-queue", body: message);

IBasicProperties properties = channel.CreateBasicProperties();
properties.Persistent = true;

for (int i = 0; i < 1000; i++)
{
    await Task.Delay(200);
    byte[] message = Encoding.UTF8.GetBytes($"Hello, World! {i}");
    channel.BasicPublish(exchange: "", routingKey: "first-queue", body: message, basicProperties: properties);
}

Console.ReadLine();