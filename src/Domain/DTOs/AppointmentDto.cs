namespace on_time_be.Domain.DTOs;

public class AppointmentDto(
    Guid id,
    string customerName,
    string customerEmail,
    string customerPhone,
    Guid salonId,
    Guid serviceId,
    Guid workerId,
    DateTime startAt,
    DateTime endAt,
    AppointmentStatus status,
    string? comments,
    bool reminderSent,
    DateTime createdAt,
    DateTime updatedAt)
{
    public Guid Id { get; init; } = id;
    public string CustomerName { get; init; } = customerName;
    public string CustomerEmail { get; init; } = customerEmail;
    public string CustomerPhone { get; init; } = customerPhone;
    public Guid SalonId { get; init; } = salonId;
    public Guid ServiceId { get; init; } = serviceId;
    public Guid WorkerId { get; init; } = workerId;
    public DateTime StartAt { get; init; } = startAt;
    public DateTime EndAt { get; init; } = endAt;
    public AppointmentStatus Status { get; init; } = status;
    public string? Comments { get; init; } = comments;
    public bool ReminderSent { get; init; } = reminderSent;
    public DateTime CreatedAt { get; init; } = createdAt;
    public DateTime UpdatedAt { get; init; } = updatedAt;
    
    
}
