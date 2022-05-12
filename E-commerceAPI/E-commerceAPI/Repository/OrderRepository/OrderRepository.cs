using E_commerceAPI.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace E_commerceAPI.Repository.OrderRepository
{
    public class OrderRepository : IOrderRepository
    {
        private ContextDB contextDB;

        public OrderRepository(ContextDB _contextDB)
        {
            contextDB = _contextDB;
        }
        public int Delete(int id)
        {
            contextDB.Orders.Remove(contextDB.Orders.Find(id));
            return contextDB.SaveChanges();
        }

        public List<Order> GetAll()
        {
            return contextDB.Orders.Include(c => c.Products).ToList();
        }

        public Order GetById(int id)
        {
            return contextDB.Orders.Include(c => c.Products).FirstOrDefault(c => c.Id == id);
        }

        public List<ProductQuantity> GetProducts(int id)
        {
            return contextDB.ProductQuantities.Include(p => p.Product).Where(p => p.ID == id).ToList();
        }
        public int Insert(Order entity)
        {
            contextDB.Orders.Add(entity);
            return contextDB.SaveChanges();
        }

        public int Update(int id, Order entity)
        {
            var Order = contextDB.Orders.FirstOrDefault(c => c.Id == id);
            if (Order != null)
            {
                Order.Products = entity.Products;
            }
            return contextDB.SaveChanges();
        }

    }
}
