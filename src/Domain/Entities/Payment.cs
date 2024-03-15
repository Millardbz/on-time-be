using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace on_time_be.Domain.Entities
{
    public class Payment(
        Guid appointmentId,
        string stripePaymentIntentId,
        long amount,
        string currency,
        PaymentStatus status)
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Appointment")]
        public Guid AppointmentId { get; set; } = appointmentId;

        [ForeignKey("User")]
        public Guid? UserId { get; set; }

        [Required]
        public string StripePaymentIntentId { get; set; } = stripePaymentIntentId;

        [Required]
        public long Amount { get; set; } = amount;

        [Required]
        [MaxLength(3)]
        public string Currency { get; set; } = currency;

        [Required]
        public PaymentStatus Status { get; set; } = status;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; }

        public string? PaymentMethod { get; set; }

        public string? StripeCustomerId { get; set; }

        public string? Metadata { get; set; }
        
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
