using on_time_be.Application.Common.Interfaces;
using on_time_be.Domain.Entities;
using on_time_be.Domain.Enums;
using on_time_be.Domain.Events.Appointment;

public record CreateAppointmentCommand : IRequest<Guid>
{
    public string CustomerName { get; init; } = string.Empty;
    public string CustomerEmail { get; init; } = string.Empty;
    public string CustomerPhone { get; init; } = string.Empty;
    public Guid SalonId { get; init; }
    public Guid ServiceId { get; init; }
    public Guid WorkerId { get; init; }
    public DateTime StartAt { get; init; }
    public DateTime EndAt { get; init; }
    public AppointmentStatus Status { get; init; } = AppointmentStatus.Pending;
    public string? Comments { get; init; }
    public bool ReminderSent { get; init; }
}

public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateAppointmentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {
        var entity = new Appointment(request.CustomerName, request.CustomerEmail, request.CustomerPhone, request.SalonId, request.ServiceId, request.WorkerId, request.StartAt, request.EndAt, request.Status)
        {
            Comments = request.Comments,
            ReminderSent = request.ReminderSent,
            Id = new Guid(),
            
        };

        entity.SendCreateEvent(new AppointmentCreatedEvent(entity));
        
        _context.AppointmentList.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
