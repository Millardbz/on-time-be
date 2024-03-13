using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace on_time_be.Domain.Entities
{
    public class Appointment(
        string customerName,
        string customerEmail,
        string customerPhone,
        Guid salonId,
        Guid serviceId,
        Guid workerId,
        DateTime startAt,
        DateTime endAt,
        AppointmentStatus status)
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string CustomerName { get; set; } = customerName;

        [Required]
        [EmailAddress]
        public string CustomerEmail { get; set; } = customerEmail;

        [Required]
        public string CustomerPhone { get; set; } = customerPhone;

        [ForeignKey("Salon")]
        public Guid SalonId { get; set; } = salonId;

        [ForeignKey("Service")]
        public Guid ServiceId { get; set; } = serviceId;

        [ForeignKey("User")]
        public Guid WorkerId { get; set; } = workerId;

        [Required]
        public DateTime StartAt { get; set; } = startAt;

        [Required]
        public DateTime EndAt { get; set; } = endAt;

        [Required]
        public AppointmentStatus Status { get; set; } = status;

        public string? Comments { get; set; } = string.Empty;

        public bool ReminderSent { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; }
        
        private readonly List<BaseEvent> _domainEvents = new List<BaseEvent>();
        public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void SendCreateEvent(BaseEvent eventItem)
        {
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(BaseEvent eventItem)
        {
            _domainEvents.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
        
    }
}
