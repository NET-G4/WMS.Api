using WMS.Domain.QueryParameters;
using WMS.Services.DTOs.Category;

namespace WMS.Services.Interfaces;

public interface ICategoryService
{
    List<CategoryDto> GetAll(CategoryQueryParameters queryParameters);
    CategoryDto GetById(int id);
    CategoryDto Create(CategoryForCreateDto categoryToCreate);
    void Update(CategoryForUpdateDto categoryToUpdate);
    void Delete(int id);
}
