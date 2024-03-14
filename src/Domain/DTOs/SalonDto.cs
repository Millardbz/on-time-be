namespace on_time_be.Domain.DTOs
{
    public class SalonDto
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string ContactInformation { get; set; }
        public string OpeningHours { get; set; }

#nullable enable
        public string? Images { get; set; }
#nullable disable

        public DepositType DepositType { get; set; }
        public decimal DepositValue { get; set; }
        public decimal PriceWithDeposit { get; set; }
        public decimal PriceWithoutDeposit { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        // Add a parameterless constructor
        public SalonDto()
        {
        }

        // Existing constructor that takes all the required parameters
        public SalonDto(Guid id, Guid ownerId, string name, string description, string location, string contactInformation, string openingHours, DepositType depositType, decimal depositValue, decimal priceWithDeposit, decimal priceWithoutDeposit, DateTime createdAt, DateTime updatedAt)
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
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }
    }
}
