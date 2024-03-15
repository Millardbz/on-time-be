using on_time_be.Domain.DTOs;
using on_time_be.Domain.Entities;

namespace on_time_be.Application.Common.Mappings;

public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<Customer, CustomerDto>();
        CreateMap<CustomerDto, Customer>()
            .ForMember(dest => dest.ContactInformation, opt => opt.MapFrom(src => src.ContactInformation));
    }
}
