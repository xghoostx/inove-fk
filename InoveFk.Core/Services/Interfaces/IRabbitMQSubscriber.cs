using InoveFk.Core.Base;

namespace InoveFk.Core.Services.Interfaces;

public interface IRabbitMQSubscriber : IDisposable
{
    void Subscribe<T>(string queue, IIntegrationEventHandler<T> handler, QueueSettings? settings);
}
