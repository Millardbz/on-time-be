using on_time_be.Application.Common.Interfaces;
using on_time_be.Domain.DTOs;

namespace on_time_be.Application.Queries.Payment;

public record GetPaymentQuery(Guid Id) : IRequest<PaymentDto>;

public class GetPaymentQueryValidator : AbstractValidator<GetPaymentQuery>
{
    public GetPaymentQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is required");
    }
}

public class GetPaymentQueryHandler : IRequestHandler<GetPaymentQuery, PaymentDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetPaymentQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaymentDto> Handle(GetPaymentQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.PaymentList
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken: cancellationToken);

        if (entity is null)
        {
            throw new NotFoundException("Payment with the given id does not exist", request.Id.ToString());
        }

        var dto = _mapper.Map<Domain.Entities.Payment, PaymentDto>(entity);

        return dto;
    }
}
