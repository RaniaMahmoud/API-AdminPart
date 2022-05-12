using E_commerceAPI.Model;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace E_commerceAPI.DTO
{
    public class StoreDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public List<Branche> Branches { set; get; }

        public StoreDTO()
        {
            Branches = new List<Branche>();
        }
    }
}
