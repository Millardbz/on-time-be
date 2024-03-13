namespace on_time_be.Domain.Events.User;

public class UserCreatedEvent : BaseEvent
{
    public UserCreatedEvent(Entities.User user)
    {
        User = user;
    }

    public Entities.User User { get; }
}
