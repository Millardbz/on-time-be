using on_time_be.Domain.DTOs;
using on_time_be.Domain.Entities;

namespace on_time_be.Application.Common.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();
    }
}
