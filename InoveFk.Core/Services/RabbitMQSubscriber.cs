using InoveFk.Core.Services.Interfaces;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using Newtonsoft.Json;
using InoveFk.Core.Base;

namespace InoveFk.Core.Services;

public class RabbitMQSubscriber : IRabbitMQSubscriber
{
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public RabbitMQSubscriber(string host)
    {
        ConnectionFactory factory = new() { HostName = host };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
    }

    public void Subscribe<T>(string queue, IIntegrationEventHandler<T> handler, QueueSettings? settings)
    {
        if (settings is not null)
        {
            CreateChannelWithSettings(settings);
        } else
        {
            CreateChannelDefault(queue);
        }

        EventingBasicConsumer consumer = new (_channel);
        consumer.Received += async (model, ea) =>
        {
            byte[] body = ea.Body.ToArray();
            string message = Encoding.UTF8.GetString(body); 
            T? @event = JsonConvert.DeserializeObject<T>(message);
            await handler.Handle(@event);
            _channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
        };

        _channel.BasicConsume(queue: queue, autoAck: false, consumer: consumer);
    }

    public void Dispose()
    {
        _channel?.Close();
        _connection?.Close();
    }

    private void CreateChannelDefault(string queue)
    {
        _channel.QueueDeclare(queue: queue,
                              durable: true,
                              exclusive: false,
                              autoDelete: false,
                              arguments: null);
    }

    private void CreateChannelWithSettings(QueueSettings settings)
    {
        _channel.QueueDeclare(queue: settings.Queue,
                              durable: settings.Durable,
                              exclusive: settings.Exclusive,
                              autoDelete: settings.AutoDelete,
                              arguments: settings.Arguments);
    }
}
