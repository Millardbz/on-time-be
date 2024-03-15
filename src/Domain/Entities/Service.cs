using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace on_time_be.Domain.Entities
{
    public class Service(Guid customerId, string name, TimeSpan duration, decimal price)
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Customer")]
        public Guid CustomerId { get; set; } = customerId;

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = name;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Required]
        public TimeSpan Duration { get; set; } = duration;

        [Required]
        public decimal Price { get; set; } = price;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; }
        
        public Guid? UserId { get; set; }
        public User? User { get; set; }
        
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
