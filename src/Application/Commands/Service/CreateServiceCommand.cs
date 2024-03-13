using on_time_be.Application.Common.Interfaces;
using on_time_be.Domain.Entities;
using on_time_be.Domain.Events.Service;

public record CreateServiceCommand : IRequest<Guid>
{
    public Guid SalonId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string? Description { get; init; }
    public TimeSpan Duration { get; init; }
    public decimal Price { get; init; }
}

public class CreateServiceCommandHandler : IRequestHandler<CreateServiceCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateServiceCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateServiceCommand request, CancellationToken cancellationToken)
    {
        var entity = new Service(request.SalonId, request.Name, request.Duration, request.Price)
        {
            Description = request.Description
        };

        entity.SendCreateEvent(new ServiceCreatedEvent(entity));
        
        _context.ServiceList.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
