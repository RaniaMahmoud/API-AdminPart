using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace E_commerceAPI.Model
{
    public class Phone
    {
        public int id { get; set; }
        public string PhoneNumber { get; set; }
        [ForeignKey("AppUser")]
        public string AppUserId { get; set; }
        [JsonIgnore]
        public AppUser AppUser { get; set; }
    }
}
