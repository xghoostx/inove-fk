using InoveFk.Core.Base;
using InoveFk.Core.Services.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace InoveFk.Core.Services;

public class RabbitMQPublisher : IRabbitMQPublisher
{
    public bool PublishMessage(object objectMessage, string host, string queue, QueueSettings? settings)
    {
        if (objectMessage is null)
        {
            throw new ArgumentNullException(nameof(objectMessage), Constants.NullObjectMessage);
        }

        try
        {
            ConnectionFactory factory = new () { HostName = host };
            using IConnection connection = factory.CreateConnection();
            using IModel channel = connection.CreateModel();

            if (settings is not null)
            {
                channel.QueueDeclare(queue: settings.Queue,
                                     durable: settings.Durable,
                                     exclusive: settings.Exclusive,
                                     autoDelete: settings.AutoDelete,
                                     arguments: settings.Arguments);
            }
            else
            {
                channel.QueueDeclare(queue: queue,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);
            }

            string message = JsonConvert.SerializeObject(objectMessage);
            byte[] body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: "",
                                 routingKey: queue,
                                 basicProperties: null,
                                 body: body);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}