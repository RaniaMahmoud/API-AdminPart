using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace E_commerceAPI.Model
{
    public class AppUser:IdentityUser
    {
        [ForeignKey("Address")]
        public int AddressId { get; set; }
        public Address Address { get; set; }
        [JsonIgnore]
        public List<Phone> Mobile_number { get; set; }
        public string Full_Name { get; set; }
        [JsonIgnore]
        public List<Order> orders { get; set; }
        public AppUser()
        {
            Mobile_number = new List<Phone>();
            orders = new List<Order>();
        }
    }
}
