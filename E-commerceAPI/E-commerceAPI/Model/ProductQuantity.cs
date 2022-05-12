using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace E_commerceAPI.Model
{
    public class ProductQuantity
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public int OrderID { get; set; }
        public int Quantity { get; set; }
        [ForeignKey("ProductID")]
        [JsonIgnore]
        public virtual Product Product { get; set; }
        [JsonIgnore]
        [ForeignKey("OrderID")]
        public virtual Order Order { get; set; }
    }
}
