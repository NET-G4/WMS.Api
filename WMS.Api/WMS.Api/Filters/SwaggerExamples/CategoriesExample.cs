using AutoMapper;
using AutoMapper.Internal;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using WMS.Services.DTOs.Category;

namespace WMS.Api.Filters.SwaggerExamples;

public class CategoriesExample : IExamplesProvider<CategoryDto>
{
    public CategoryDto GetExamples()
    {
        return new CategoryDto
        {
            Id = 1,
            Name = "Drinks",
            Description = "Cold drinks."
        };
    }
}
