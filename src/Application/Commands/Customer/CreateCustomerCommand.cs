using on_time_be.Application.Common.Interfaces;
using on_time_be.Domain.Enums;

namespace on_time_be.Application.Commands.Customer;

public record CreateCustomerCommand(string Name, string Description, string Location, string ContactInformation, string OpeningHours) : IRequest<Guid>
{
    public Guid Id { get; init; }
    public Guid OwnerId { get; init; }
    public decimal DepositValue { get; init; }
    public decimal PriceWithDeposit { get; init; }
    public decimal PriceWithoutDeposit { get; init; }
    public DepositType DepositType { get; init; }
}

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateCustomerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = new Domain.Entities.Customer(request.Id, request.OwnerId, request.Name, request.Description, request.Location, request.ContactInformation, request.OpeningHours, request.DepositType, request.DepositValue, request.PriceWithDeposit, request.PriceWithoutDeposit); 

        _context.CustomerList.Add(customer);

        await _context.SaveChangesAsync(cancellationToken);

        return customer.Id;
    }
}
