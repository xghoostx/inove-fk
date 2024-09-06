namespace InoveFk.Core.Services.Interfaces;

public interface IIntegrationEventHandler<T>
{
    Task Handle(T @event);
}
