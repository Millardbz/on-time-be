using on_time_be.Application.Common.Interfaces;
using on_time_be.Domain.DTOs;


namespace on_time_be.Application.Queries.Service;

public record GetServiceQuery(Guid Id) : IRequest<ServiceDto>;

public class GetServiceQueryValidator : AbstractValidator<GetServiceQuery>
{
    public GetServiceQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
    }
}

public class GetServiceQueryHandler : IRequestHandler<GetServiceQuery, ServiceDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetServiceQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceDto> Handle(GetServiceQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.ServiceList
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException("Service with the given id does not exist", request.Id.ToString());
        }

        var dto = _mapper.Map<Domain.Entities.Service, ServiceDto>(entity);

        return dto;
    }
}
