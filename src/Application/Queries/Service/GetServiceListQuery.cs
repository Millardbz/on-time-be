using on_time_be.Application.Common.Interfaces;
using on_time_be.Domain.DTOs;
using on_time_be.Application.Common.Mappings;
using on_time_be.Application.Common.Models;

namespace on_time_be.Application.Queries.Service;

public record GetServiceListQuery() : IRequest<PaginatedList<ServiceDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetServiceListQueryValidator : AbstractValidator<GetServiceListQuery>
{
    public GetServiceListQueryValidator()
    {
        RuleFor(query => query.PageNumber).GreaterThanOrEqualTo(1).WithMessage("PageNumber must be at least greater than or equal to 1.");
        RuleFor(query => query.PageSize).GreaterThanOrEqualTo(1).LessThanOrEqualTo(100).WithMessage("PageSize must be at least greater than or equal to 1 and less than or equal to 100.");
    }
}

public class GetServiceListQueryHandler : IRequestHandler<GetServiceListQuery, PaginatedList<ServiceDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetServiceListQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ServiceDto>> Handle(GetServiceListQuery request, CancellationToken cancellationToken)
    {
        var query = _context.ServiceList.AsQueryable();

        return await query.OrderBy(x => x.Name)
            .ProjectTo<ServiceDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}
