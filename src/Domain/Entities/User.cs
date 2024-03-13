using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using on_time_be.Domain.Enums; // Import the namespace where UserRole enum is located

namespace on_time_be.Domain.Entities
{
    public class User(Guid id, string name, string email, string password, UserRole role, Guid salonId)
    {
        [Key] public Guid Id { get; set; } = id;

        [ForeignKey("Salon")] public Guid SalonId { get; set; } = salonId;

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = name;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = email;

        [Required]
        public string Password { get; set; } = password;

        [Required]
        public UserRole Role { get; set; } = role; 

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
