using AutoMapper;
using WMS.Domain.Entities;
using WMS.Services.DTOs.Sale;

namespace WMS.Services.Mappings;

public class SaleMappings : Profile
{
    public SaleMappings()
    {
        CreateMap<Sale, SaleDto>()
            .ForMember(x => x.Customer, r => r.MapFrom(e => GetCustomerName(e)));
        CreateMap<SaleForCreateDto, Sale>();
        CreateMap<SaleForUpdateDto, Sale>();
    }

    private static string GetCustomerName(Sale sale)
    {
        var customer = sale?.Customer;

        if (customer is null)
        {
            return string.Empty;
        }

        if (customer.LastName is null)
        {
            return customer.FirstName;
        }

        return customer.FirstName + " " + customer.LastName;
    }
}
