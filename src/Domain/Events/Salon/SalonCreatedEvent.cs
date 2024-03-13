namespace on_time_be.Domain.Events.Salon;

public class SalonCreatedEvent : BaseEvent
{
    public SalonCreatedEvent(Entities.Salon salon)
    {
        Salon = salon;
    }

    public Entities.Salon Salon { get; }
}
