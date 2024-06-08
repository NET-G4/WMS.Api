using AutoMapper;
using WMS.Domain.Entities;
using WMS.Services.DTOs.SaleItem;

namespace WMS.Services.Mappings;

public class SaleItemMappings : Profile
{
    public SaleItemMappings()
    {
        CreateMap<SaleItem, SaleItemDto>();
        CreateMap<SaleItemForCreateDto, SaleItem>();
        CreateMap<SaleItemForUpdateDto, SaleItem>();
    }
}
