using E_commerceAPI.Model;
using System.Collections.Generic;
using System.Linq;

namespace E_commerceAPI.Repository.BrancheRepository
{
    public class BrancheRepository : IBrancheRepository
    {
        ContextDB contextDB;
        public BrancheRepository(ContextDB _contextDB)
        {
            contextDB = _contextDB;
        }
        public int Delete(int id)
        {
            contextDB.Branche.Remove(contextDB.Branche.Find(id));
            return contextDB.SaveChanges();
        }

        public List<Branche> GetAll()
        {
            return contextDB.Branche.ToList();
        }

        public Branche GetById(int id)
        {
            return contextDB.Branche.FirstOrDefault(c => c.id == id);
        }

        public int Insert(Branche entity)
        {
            contextDB.Branche.Add(entity);
            return contextDB.SaveChanges();
        }

        public int Update(int id, Branche entity)
        {
            var branche = contextDB.Branche.FirstOrDefault(c => c.id == id);
            if (branche != null)
            {
                branche.Name = entity.Name;
                branche.StoreId = entity.StoreId;
            }
            return contextDB.SaveChanges();
        }
    }
}
