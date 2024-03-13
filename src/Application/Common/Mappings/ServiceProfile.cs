using on_time_be.Domain.DTOs;
using on_time_be.Domain.Entities;

namespace on_time_be.Application.Common.Mappings;

public class ServiceProfile : Profile
{
    public ServiceProfile()
    {
        CreateMap<Service, ServiceDto>();
    }
}
