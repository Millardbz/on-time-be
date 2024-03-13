namespace on_time_be.Domain.Events.Appointment;

public class AppointmentCreatedEvent : BaseEvent
{
    public AppointmentCreatedEvent(Entities.Appointment appointment)
    {
        Appointment = appointment;
    }

    public Entities.Appointment Appointment { get; }
}
