using AutoMapper;
using on_time_be.Domain.Entities;

namespace on_time_be.Domain.DTOs;

public class UserDto(string name, string email, Guid id)
{
    public Guid Id { get; init; } = id;

    public string Name { get; init; } = name ?? throw new ArgumentNullException(nameof(name));

    public string Email { get; init; } = email ?? throw new ArgumentNullException(nameof(email));
    
}
