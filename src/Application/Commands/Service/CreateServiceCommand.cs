using on_time_be.Application.Common.Interfaces;
using on_time_be.Domain.Events.Service;

namespace on_time_be.Application.Commands.Service;

public record CreateServiceCommand : IRequest<Guid>
{
    public Guid CustomerId { get; init; }
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
        var entity = new Domain.Entities.Service(request.CustomerId, request.Name, request.Duration, request.Price)
        {
            Description = request.Description
        };

        entity.SendCreateEvent(new ServiceCreatedEvent(entity));
        
        _context.ServiceList.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
