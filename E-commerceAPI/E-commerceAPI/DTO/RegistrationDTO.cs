using E_commerceAPI.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace E_commerceAPI.DTO
{
    public class RegistrationDTO
    {
        public string password { get; set; }
        [DataType("Email")]
        public string Email { get; set; }
        public Address Address { get; set; }
        public List<string> Mobile_number { get; set; }
        public string Full_Name { get; set; }
    }
}
