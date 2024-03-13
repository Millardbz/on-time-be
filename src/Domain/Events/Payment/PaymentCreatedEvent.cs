namespace on_time_be.Domain.Events.Payment;

public class PaymentCreatedEvent : BaseEvent
{
    public PaymentCreatedEvent(Entities.Payment payment)
    {
        Payment = payment;
    }

    public Entities.Payment Payment { get; }
}
