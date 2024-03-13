using on_time_be.Application.Common.Interfaces;
using on_time_be.Domain.DTOs;

namespace on_time_be.Application.Queries.User;

public record GetUserQuery(Guid Id) : IRequest<UserDto>;

public class GetUserQueryValidator : AbstractValidator<GetUserQuery>
{
    public GetUserQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
    }
}

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetUserQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.UserList
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException("User with the given id does not exist", request.Id.ToString());
        }

        var dto = _mapper.Map<Domain.Entities.User, UserDto>(entity);

        return dto;
    }
}
