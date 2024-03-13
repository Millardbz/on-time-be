using on_time_be.Domain.DTOs;
using on_time_be.Domain.Entities;

namespace on_time_be.Application.Common.Mappings;

public class SalonProfile : Profile
{
    public SalonProfile()
    {
        CreateMap<Salon, SalonDto>();
        CreateMap<SalonDto, Salon>()
            .ForMember(dest => dest.ContactInformation, opt => opt.MapFrom(src => src.ContactInformation));
    }
}
