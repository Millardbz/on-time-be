namespace on_time_be.Domain.Events.Customer;

public class CustomerCreatedEvent : BaseEvent
{
    public CustomerCreatedEvent(Entities.Customer customer)
    {
        Customer = customer;
    }

    public Entities.Customer Customer { get; }
}
