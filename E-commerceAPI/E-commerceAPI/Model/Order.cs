using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace E_commerceAPI.Model
{
    public class Order
    {
        public int Id { get; set; }

        [ForeignKey("AppUser")]
        public string AppUserId { get; set; }
        [JsonIgnore]
        public AppUser AppUser { get; set; }
        public List<ProductQuantity> Products { get; set; }
        public int TotalPrice { set; get; }
        public Order()
        {
            Products = new List<ProductQuantity>();
        }
    }
}
