using System.Collections.Generic;

namespace E_commerceAPI.Repository
{
    public interface IRepository<T>
    {
        T GetById(int id);
        List<T> GetAll();
        int Insert(T entity);
        int Update(int id, T entity);
        int Delete(int id);
    }
}
