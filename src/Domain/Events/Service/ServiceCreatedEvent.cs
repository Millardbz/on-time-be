namespace on_time_be.Domain.Events.Service;

public class ServiceCreatedEvent : BaseEvent
{
    public ServiceCreatedEvent(Entities.Service service)
    {
        Service = service;
    }

    public Entities.Service Service { get; }
}
