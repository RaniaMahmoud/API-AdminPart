using E_commerceAPI.Model;
using System.Collections.Generic;
using System.Linq;

namespace E_commerceAPI.Repository.AdminRepo
{
    public class AdminRepository : IAdminRepository
    {
        ContextDB contextDB;
        public AdminRepository(ContextDB _contextDB)
        {
            contextDB = _contextDB;
        }
        public int Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<Admin> GetAll()
        {
            return contextDB.Admin.ToList();
        }

        public Admin GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public int Insert(Admin entity)
        {
            throw new System.NotImplementedException();
        }

        public int Update(int id, Admin entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
