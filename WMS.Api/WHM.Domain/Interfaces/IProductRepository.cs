using WMS.Domain.Entities;
using WMS.Domain.QueryParameters;

namespace WMS.Domain.Interfaces
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        List<Product> FindAll(ProductQueryParameters queryParameters);
    }
}
