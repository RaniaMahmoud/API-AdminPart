using E_commerceAPI.Model;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace E_commerceAPI.DTO
{
    public class CategoryDTO
    {
        public int id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<Product> products { get; set; }
    }
}
