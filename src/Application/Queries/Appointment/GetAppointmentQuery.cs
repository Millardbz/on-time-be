using on_time_be.Application.Common.Interfaces;
using on_time_be.Domain.DTOs;

namespace on_time_be.Application.Queries.Appointment;

public record GetAppointmentQuery(Guid Id) : IRequest<AppointmentDto>;

public class GetAppointmentQueryValidator : AbstractValidator<GetAppointmentQuery>
{
    public GetAppointmentQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
    }
}

public class GetAppointmentQueryHandler : IRequestHandler<GetAppointmentQuery, AppointmentDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAppointmentQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AppointmentDto> Handle(GetAppointmentQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.AppointmentList
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException("Appointment with the given id does not exist", request.Id.ToString());
        }

        var dto = _mapper.Map<Domain.Entities.Appointment, AppointmentDto>(entity);

        return dto;
    }
}
