using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace E_commerceAPI.Model
{
    public class Category
    {
        public int id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<Product> products { get; set; }
        public Category()
        {
            products = new HashSet<Product>();
        }
    }
}
