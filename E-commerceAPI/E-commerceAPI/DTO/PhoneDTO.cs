using E_commerceAPI.Model;
using System.Text.Json.Serialization;

namespace E_commerceAPI.DTO
{
    public class PhoneDTO
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string AppUserId { get; set; }
        [JsonIgnore]
        public AppUser AppUser { get; set; }
    }
}
