using WMS.Domain.QueryParameters;
using WMS.Services.Common;
using WMS.Services.DTOs.Product;

namespace WMS.Services.Interfaces;

public interface IProductService
{
    PaginatedList<ProductDto> GetAll(ProductQueryParameters queryParameters);
    ProductDto GetById(int id);
    ProductDto Create(ProductForCreateDto productToCreate);
    void Update(ProductForUpdateDto productToUpdate);
    void Delete(int id);
}
