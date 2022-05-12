using E_commerceAPI.Model;
using System.Collections.Generic;
using System.Linq;

namespace E_commerceAPI.Repository.PhoneRepository
{
    public class PhoneRepository : IPhoneRepository
    {
        ContextDB contextDB;
        public PhoneRepository(ContextDB _contextDB)
        {
            contextDB = _contextDB;
        }
        public int Delete(int id)
        {
            contextDB.Phone.Remove(contextDB.Phone.Find(id));
            return contextDB.SaveChanges();
        }

        public List<Phone> GetAll()
        {
            return contextDB.Phone.ToList();
        }

        public List<Phone> GetPhones(string id)
        {
            return contextDB.Phone.Where(p => p.AppUserId == id).ToList();
        }

        public Phone GetById(int id)
        {
            return contextDB.Phone.FirstOrDefault(c => c.id == id);
        }
        public Phone GetByNumber(string number)
        {
            return contextDB.Phone.FirstOrDefault(c => c.PhoneNumber == number);
        }

        public int Insert(Phone entity)
        {
            contextDB.Phone.Add(entity);
            return contextDB.SaveChanges();
        }

        public int Update(int id, Phone entity)
        {
            var phone = contextDB.Phone.FirstOrDefault(c => c.id == id);
            if (phone != null)
            {
                phone.PhoneNumber = entity.PhoneNumber;
                phone.AppUserId = entity.AppUserId;
            }
            return contextDB.SaveChanges();
        }
    }
}
