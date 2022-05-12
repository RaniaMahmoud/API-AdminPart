using E_commerceAPI.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace E_commerceAPI.Repository.StoreRepository
{
    public class StoreRepository : IStoreRepository
    {
        ContextDB contextDB;
        public StoreRepository(ContextDB _contextDB)
        {
            contextDB = _contextDB;
        }
        public int Delete(int id)
        {
            contextDB.Store.Remove(contextDB.Store.Find(id));
            return contextDB.SaveChanges();
        }

        public List<Store> GetAll()
        {
            return contextDB.Store.Include((s)=>s.Branches).ToList();
        }

        public Store GetById(int id)
        {
            return contextDB.Store.Include((s) => s.Branches).FirstOrDefault(c => c.id == id);
        }

        public int Insert(Store entity)
        {
            contextDB.Store.Add(entity);
            return contextDB.SaveChanges();
        }

        public int Update(int id, Store entity)
        {
            var store = contextDB.Store.FirstOrDefault(c => c.id == id);
            if (store != null)
            {
                store.Name = entity.Name;
            }
            return contextDB.SaveChanges();
        }
    }
}
