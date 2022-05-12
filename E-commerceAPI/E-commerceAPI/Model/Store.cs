using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace E_commerceAPI.Model
{
    public class Store
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        [JsonIgnore]
        public List<Branche> Branches { set; get; }
        public Store()
        {
            Branches = new List<Branche>();
        }
    }
}
