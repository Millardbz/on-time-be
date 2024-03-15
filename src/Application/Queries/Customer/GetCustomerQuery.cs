using on_time_be.Application.Common.Interfaces;
using on_time_be.Domain.DTOs;

namespace on_time_be.Application.Queries.Customer;

public record GetCustomerQuery(Guid Id) : IRequest<CustomerDto>;

public class GetCustomerQueryValidator : AbstractValidator<GetCustomerQuery>
{
    public GetCustomerQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
    }
}

public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, CustomerDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCustomerQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CustomerDto> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.CustomerList
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException("Customer with the given id does not exist", request.Id.ToString());
        }

        var dto = _mapper.Map<Domain.Entities.Customer, CustomerDto>(entity);

        return dto;
    }
}
