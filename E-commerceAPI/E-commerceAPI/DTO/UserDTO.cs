using E_commerceAPI.Model;
using System.Collections.Generic;

namespace E_commerceAPI.DTO
{
    public class UserDTO
    {
        public string id { set; get; }
        public string Full_Name { set; get; }
        public string Email { set; get; }
        public List<string> Mobile_number { set; get; }
        public Address Address { get; set; }
        public string password { get; set; }
        public UserDTO()
        {
            Mobile_number = new List<string>();
        }

    }
}
