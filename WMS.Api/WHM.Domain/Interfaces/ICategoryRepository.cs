using WMS.Domain.Entities;
using WMS.Domain.QueryParameters;

namespace WMS.Domain.Interfaces;

public interface ICategoryRepository : IRepositoryBase<Category>
{
    List<Category> FindAll(CategoryQueryParameters queryParameters);
}
