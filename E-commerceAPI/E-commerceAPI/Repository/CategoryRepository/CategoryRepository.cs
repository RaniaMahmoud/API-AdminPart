using E_commerceAPI.Model;
using E_commerceAPI.Repository.CategoryRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace E_commerceAPI.Repository.CategoryRepository
{
    public class CategoryRepository : ICategoryRepository
    {
        private ContextDB contextDB;

        public CategoryRepository(ContextDB _contextDB)
        {
            contextDB = _contextDB;
        }
        public int Delete(int id)
        {
            contextDB.Category.Remove(contextDB.Category.Find(id));
            return contextDB.SaveChanges();
        }

        public List<Category> GetAll()
        {
            return contextDB.Category.Include(c => c.products).ToList();
        }

        public Category GetById(int id)
        {
            return contextDB.Category.Include(c=>c.products).FirstOrDefault(c => c.id == id);
        }

        public List<Product> GetProducts(int id)
        {
            return contextDB.Products.Where(p => p.CategoryID == id).ToList();
        }
        public int Insert(Category entity)
        {
            contextDB.Category.Add(entity);
            return contextDB.SaveChanges();
        }

        public int Update(int id, Category entity)
        {
            var category = contextDB.Category.FirstOrDefault(c => c.id == id);
            if (category != null)
            {
                category.Name=entity.Name;
            }
            return contextDB.SaveChanges();
        }
    }
}
