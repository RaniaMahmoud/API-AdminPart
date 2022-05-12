using E_commerceAPI.Model;
using E_commerceAPI.Repository.ProductRepository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace E_commerceAPI.Repository.ProductRepository
{
    public class ProductRepository : IProductRepository
    {
        ContextDB context;
        public ProductRepository(ContextDB _context)
        {
            context = _context;
        }

        public int Delete(int id)
        {
            context.Products.Remove(context.Products.Find(id));
            return context.SaveChanges();
        }

        public List<Product> GetAll()
        {
            return context.Products.Include((p)=>p.category).ToList();
        }

        public Product GetById(int id)
        {
            return context.Products.Include((p) => p.category).FirstOrDefault(p => p.id == id);
        }

        public int Insert(Product entity)
        {
            if(entity.Name != null)
            {
                context.Products.Add(entity);
                return context.SaveChanges();
            }
            else
            {
                return 0;
            }
        }

        public int Update(int id, Product entity)
        {
            Product Model = context.Products.FirstOrDefault(d => d.id == id);
            Model.Name = entity.Name;
            Model.CategoryID = entity.CategoryID;
            Model.CreationDate = entity.CreationDate;
            Model.Discription = entity.Discription;
            if(entity.Image != null)
            {
                Model.Image = entity.Image;
            }
            Model.Price = entity.Price;
            Model.Quantity = entity.Quantity;
            return context.SaveChanges();

        }
    }
}
