using AutoMapper;
using WMS.Domain.Entities;
using WMS.Services.DTOs.Product;

namespace WMS.Services.Mappings;

public class ProductMappings : Profile
{
    public ProductMappings()
    {
        CreateMap<Product, ProductDto>()
            .ForMember(dto => dto.Category, e => e.MapFrom(r => r.Category.Name));
        CreateMap<ProductForCreateDto, Product>();
        CreateMap<ProductForUpdateDto, Product>();
    }
}
