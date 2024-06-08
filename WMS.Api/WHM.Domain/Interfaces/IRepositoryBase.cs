using WMS.Domain.Common;

namespace WMS.Domain.Interfaces;

public interface IRepositoryBase<T> where T : EntityBase
{
    List<T> FindAll();
    T FindById(int id);
    T Create(T entity);
    T Update(T entity);
    void Delete(int id);
}
