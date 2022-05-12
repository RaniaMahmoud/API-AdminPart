using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace E_commerceAPI.Model
{
    public class Card
    {
        public int ID { get; set; }
        public double TotalPrice { get; set; }
        public string UserID { get; set; }
        [ForeignKey("UserID")]
        [JsonIgnore]
        public virtual AppUser Customer { get; set; }
        [JsonIgnore]
        public virtual List<ProductQuantity> ProductQuantity { get; set; }
        public Card()
        {
            ProductQuantity = new List<ProductQuantity>();
        }
    }
}
