using on_time_be.Application.Common.Interfaces;
using on_time_be.Domain.Enums;
using on_time_be.Domain.Events.User;

namespace on_time_be.Application.Commands.User;

public record CreateUserCommand : IRequest<Guid>
{
    public Guid Id { get; init; }
    public Guid CustomerId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;

    public string Password { get; init; } = string.Empty;

    public UserRole Role { get; init; } = UserRole.Staff;
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateUserCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.User(request.Id, request.Name, request.Email, request.Password, request.Role, request.CustomerId);
        
        entity.SendCreateEvent(new UserCreatedEvent(entity));

        _context.UserList.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
