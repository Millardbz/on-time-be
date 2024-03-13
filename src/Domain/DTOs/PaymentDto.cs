namespace on_time_be.Domain.DTOs;

public record PaymentDto(
    Guid id,
    Guid appointmentId,
    Guid? userId,
    string stripePaymentIntentId,
    long amount,
    string currency,
    PaymentStatus status,
    DateTime created,
    DateTime updated,
    string? paymentMethod,
    string? stripeCustomerId,
    string? metadata)
{
    public Guid Id { get; init; } = id;
    public Guid AppointmentId { get; init; } = appointmentId;
    public Guid? UserId { get; init; } = userId;
    public string StripePaymentIntentId { get; init; } = stripePaymentIntentId;
    public long Amount { get; init; } = amount;
    public string Currency { get; init; } = currency;
    public PaymentStatus Status { get; init; } = status;
    public DateTime Created { get; init; } = created;
    public DateTime Updated { get; init; } = updated;
    public string? PaymentMethod { get; init; } = paymentMethod;
    public string? StripeCustomerId { get; init; } = stripeCustomerId;
    public string? Metadata { get; init; } = metadata;
}
