using AutoMapper;
using WMS.Domain.Entities;
using WMS.Services.DTOs.Category;

namespace WMS.Services.Mappings;

public class CategoryMappings : Profile
{
    public CategoryMappings()
    {
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<CategoryForCreateDto, Category>();
        CreateMap<CategoryForUpdateDto, Category>();
    }
}
