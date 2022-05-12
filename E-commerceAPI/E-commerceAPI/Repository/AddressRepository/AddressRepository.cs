using E_commerceAPI.Model;
using System.Collections.Generic;
using System.Linq;

namespace E_commerceAPI.Repository.AddressRepository
{
    public class AddressRepository : IAddressRepository
    {
        ContextDB db;
        public AddressRepository(ContextDB contextDB)
        {
            db = contextDB;
        }
        public int Delete(int id)
        {
            db.Address.Remove(db.Address.Find(id));
            return db.SaveChanges();
        }

        public List<Address> GetAll()
        {
            return db.Address.ToList();
        }

        public Address GetById(int id)
        {
            return db.Address.FirstOrDefault(x => x.id == id);
        }

        public int Insert(Address entity)
        {
            db.Address.Add(entity);
            return db.SaveChanges();
        }

        public int Update(int id, Address entity)
        {
            var address = db.Address.FirstOrDefault(c => c.id == id);
            if (address != null)
            {
                address.City = entity.City;
                address.postalCode = entity.postalCode;
                address.street = entity.street;
            }
            return db.SaveChanges();
        }
    }
}
