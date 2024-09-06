using InoveFk.Core.Base;

namespace InoveFk.Core.Services.Interfaces;

public interface IRabbitMQPublisher
{
    public bool PublishMessage(
        object objectMessage, string host, string queue, QueueSettings? settings);
}
