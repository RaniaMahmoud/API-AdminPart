using E_commerceAPI.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_commerceAPI.Repository.AdminRepo
{
    public interface IAdminRepository:IRepository<Admin>
    {
        Task<List<Admin>> GetAllAdmin();
    }
}
