using E_commerceAPI.Model;
using System.Collections.Generic;

namespace E_commerceAPI.Repository.PhoneRepository
{
    public interface IPhoneRepository:IRepository<Phone>
    {
        List<Phone> GetPhones(string id);
    }
}
