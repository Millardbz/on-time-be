namespace on_time_be.Domain.DTOs;

public record ServiceDto(
    Guid Id,
    Guid CustomerId,
    string Name,
    string? Description,
    TimeSpan Duration,
    decimal Price,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    Guid UserId);
