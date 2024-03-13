using on_time_be.Application.Common.Interfaces;
using on_time_be.Domain.DTOs;

namespace on_time_be.Application.Queries.Salon;

public record GetSalonQuery(Guid Id) : IRequest<SalonDto>;

public class GetSalonQueryValidator : AbstractValidator<GetSalonQuery>
{
    public GetSalonQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
    }
}

public class GetSalonQueryHandler : IRequestHandler<GetSalonQuery, SalonDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetSalonQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SalonDto> Handle(GetSalonQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.SalonList
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException("Salon with the given id does not exist", request.Id.ToString());
        }

        var dto = _mapper.Map<Domain.Entities.Salon, SalonDto>(entity);

        return dto;
    }
}
