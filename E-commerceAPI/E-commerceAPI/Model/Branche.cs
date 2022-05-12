using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace E_commerceAPI.Model
{
    public class Branche
    {
        public int id { get; set; }
        public string Name { get; set; }
        [ForeignKey("Store")]
        public int StoreId { get; set; }
        [JsonIgnore]
        public Store Store { get; set; }
    }
}
