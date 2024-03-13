using on_time_be.Application.Common.Interfaces;
using on_time_be.Domain.DTOs;
using on_time_be.Application.Common.Mappings;
using on_time_be.Application.Common.Models;

namespace on_time_be.Application.Queries.Payment;

public record GetPaymentListQuery() : IRequest<PaginatedList<PaymentDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetPaymentListQueryValidator : AbstractValidator<GetPaymentListQuery>
{
    public GetPaymentListQueryValidator()
    {
        RuleFor(query => query.PageNumber).GreaterThanOrEqualTo(1).WithMessage("PageNumber must be at least greater than or equal to 1.");
        RuleFor(query => query.PageSize).GreaterThanOrEqualTo(1).LessThanOrEqualTo(100).WithMessage("PageSize must be at least greater than or equal to 1 and less than or equal to 100.");
    }
}

public class GetPaymentListQueryHandler : IRequestHandler<GetPaymentListQuery, PaginatedList<PaymentDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetPaymentListQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<PaymentDto>> Handle(GetPaymentListQuery request, CancellationToken cancellationToken)
    {
        var query = _context.PaymentList.AsQueryable();

        return await query.OrderBy(x => x.Created)
            .ProjectTo<PaymentDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}
