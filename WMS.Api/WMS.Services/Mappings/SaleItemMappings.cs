using AutoMapper;
using WMS.Domain.Entities;
using WMS.Services.DTOs.SaleItem;

namespace WMS.Services.Mappings;

public class SaleItemMappings : Profile
{
    public SaleItemMappings()
    {
        CreateMap<SaleItem, SaleItemDto>()
            .ForMember(dto => dto.Product, e => e.MapFrom(e => e.Product.Name));
        CreateMap<SaleItemForCreateDto, SaleItem>();
        CreateMap<SaleItemForUpdateDto, SaleItem>();
    }
}
