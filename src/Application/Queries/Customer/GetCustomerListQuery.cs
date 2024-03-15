using on_time_be.Application.Common.Interfaces;
using on_time_be.Application.Common.Mappings;
using on_time_be.Application.Common.Models;
using on_time_be.Domain.DTOs;

namespace on_time_be.Application.Queries.Customer;

public record GetCustomerListQuery() : IRequest<PaginatedList<CustomerDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetCustomerListQueryValidator : AbstractValidator<GetCustomerListQuery>
{
    public GetCustomerListQueryValidator()
    {
        RuleFor(query => query.PageNumber).GreaterThanOrEqualTo(1).WithMessage("PageNumber must be at least greater than or equal to 1.");
        RuleFor(query => query.PageSize).GreaterThanOrEqualTo(1).LessThanOrEqualTo(100).WithMessage("PageSize must be at least greater than or equal to 1 and less than or equal to 100.");
    }
}

public class GetCustomerListQueryHandler : IRequestHandler<GetCustomerListQuery, PaginatedList<CustomerDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCustomerListQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<CustomerDto>> Handle(GetCustomerListQuery request, CancellationToken cancellationToken)
    {
        var query = _context.CustomerList.AsQueryable();

        return await query.OrderBy(x => x.Name)
            .ProjectTo<CustomerDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}
