using on_time_be.Application.Common.Interfaces;
using on_time_be.Domain.DTOs;
using on_time_be.Application.Common.Mappings;
using on_time_be.Application.Common.Models;

namespace on_time_be.Application.Queries.Appointment;
public record GetAppointmentListQuery() : IRequest<PaginatedList<AppointmentDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetAppointmentListQueryValidator : AbstractValidator<GetAppointmentListQuery>
{
    public GetAppointmentListQueryValidator()
    {
        RuleFor(query => query.PageNumber).GreaterThanOrEqualTo(1).WithMessage("PageNumber must be at least greater than or equal to 1.");
        RuleFor(query => query.PageSize).GreaterThanOrEqualTo(1).LessThanOrEqualTo(100).WithMessage("PageSize must be at least greater than or equal to 1 and less than or equal to 100.");
    }
}

public class GetAppointmentListQueryHandler : IRequestHandler<GetAppointmentListQuery, PaginatedList<AppointmentDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAppointmentListQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<AppointmentDto>> Handle(GetAppointmentListQuery request, CancellationToken cancellationToken)
    {
        var query = _context.AppointmentList.AsQueryable();

        return await query.OrderBy(x => x.StartAt)
            .ProjectTo<AppointmentDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}
