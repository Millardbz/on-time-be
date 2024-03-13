using on_time_be.Application.Common.Interfaces;
using on_time_be.Domain.Entities;
using on_time_be.Domain.Enums;
using on_time_be.Domain.Events.Payment;
using on_time_be.Domain.Events.User;

public record CreatePaymentCommand : IRequest<Guid>
{
    public Guid AppointmentId { get; init; }
    public string StripePaymentIntentId { get; init; } = string.Empty;
    public long Amount { get; init; }
    public string Currency { get; init; } = string.Empty;
    public PaymentStatus Status { get; init; }
    public string? PaymentMethod { get; init; }
    public string? StripeCustomerId { get; init; }
    public string? Metadata { get; init; }
}

public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreatePaymentCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        var entity = new Payment(request.AppointmentId, request.StripePaymentIntentId, request.Amount, request.Currency, request.Status)
        {
            PaymentMethod = request.PaymentMethod,
            StripeCustomerId = request.StripeCustomerId,
            Metadata = request.Metadata
        };

        entity.SendCreateEvent(new PaymentCreatedEvent(entity));
        
        _context.PaymentList.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
