using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using on_time_be.Domain.Enums;
using on_time_be.Domain.Events;

namespace on_time_be.Domain.Entities
{
    public class Salon
    {
        [Key] 
        public Guid Id { get; set; }

        [ForeignKey("User")]
        public Guid OwnerId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string ContactInformation { get; set; }

        [Required]
        public string OpeningHours { get; set; }

        public string? Images { get; set; }

        public DepositType DepositType { get; set; }

        public decimal DepositValue { get; set; }

        public decimal PriceWithDeposit { get; set; }

        public decimal PriceWithoutDeposit { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        private readonly List<BaseEvent> _domainEvents = new List<BaseEvent>();
        public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

        public Salon(
            Guid id,
            Guid ownerId,
            string name,
            string description,
            string location,
            string contactInformation,
            string openingHours,
            DepositType depositType,
            decimal depositValue,
            decimal priceWithDeposit,
            decimal priceWithoutDeposit)
        {
            Id = id;
            OwnerId = ownerId;
            Name = name;
            Description = description;
            Location = location;
            ContactInformation = contactInformation;
            OpeningHours = openingHours;
            DepositType = depositType;
            DepositValue = depositValue;
            PriceWithDeposit = priceWithDeposit;
            PriceWithoutDeposit = priceWithoutDeposit;
            CreatedAt = DateTime.UtcNow;
        }

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
