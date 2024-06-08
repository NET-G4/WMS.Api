using AutoMapper;
using WMS.Domain.Entities;
using WMS.Services.DTOs.Customer;

namespace WMS.Services.Mappings;

public class CustomerMappings : Profile
{
    public CustomerMappings()
    {
        CreateMap<Customer, CustomerDto>()
            .ForMember(x => x.FullName, r => r.MapFrom(e => GetFullName(e)));
        CreateMap<CustomerForCreateDto, Customer>();
        CreateMap<CustomerForUpdateDto, Customer>();
    }

    private string GetFullName(Customer customer)
    {
        if (customer.LastName == null)
        {
            return customer.FirstName;
        }

        return customer.FirstName + " " + customer.LastName;
    }
}
