﻿// Creating Connection
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

// Creating Queue
channel.QueueDeclare(queue: "first-queue", exclusive: false);

// Read Message(s) from Queue
EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: "first-queue", autoAck: false, consumer: consumer);

consumer.Received += (sender, e) =>
{
    // In this block, we are processing the message coming from the Producer
    // e.Body.Span or e.Body.ToArray(): These methods are provides the byte data from queue to us.
    
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
};

Console.ReadLine();