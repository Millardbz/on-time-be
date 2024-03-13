using on_time_be.Application.Common.Interfaces;
using on_time_be.Domain.Enums;
using on_time_be.Domain.Events.Salon;

namespace on_time_be.Application.Commands.Salon;

public record CreateSalonCommand(string Name, string Description, string Location, string ContactInformation, string OpeningHours) : IRequest<Guid>
{
    public Guid Id { get; init; }
    public Guid OwnerId { get; init; }
    public decimal DepositValue { get; init; }
    public decimal PriceWithDeposit { get; init; }
    public decimal PriceWithoutDeposit { get; init; }
    public DepositType DepositType { get; init; }
}

public class CreateSalonCommandHandler : IRequestHandler<CreateSalonCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateSalonCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateSalonCommand request, CancellationToken cancellationToken)
    {
        var salon = new Domain.Entities.Salon(request.Id, request.OwnerId, request.Name, request.Description, request.Location, request.ContactInformation, request.OpeningHours, request.DepositType, request.DepositValue, request.PriceWithDeposit, request.PriceWithoutDeposit); 

        _context.SalonList.Add(salon);

        await _context.SaveChangesAsync(cancellationToken);

        return salon.Id;
    }
}
