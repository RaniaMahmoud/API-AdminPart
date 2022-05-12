
using E_commerceAPI.Model;
using E_commerceAPI.Repository;
using System.Collections.Generic;

namespace E_commerceAPI.Repository.CategoryRepository
{
    public interface ICategoryRepository: IRepository<Category>
    {
        List<Product> GetProducts(int id);
    }
}
