using E_commerceAPI.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Text.Json.Serialization;

namespace E_commerceAPI.DTO
{
    public class ProductDTO
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Discription { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        public DateTime CreationDate { get; set; }
        //public IFormFile file { get; set; }

        [ForeignKey("category")]
        public int CategoryID { get; set; }
        [JsonIgnore]
        public virtual Category category { get; set; }
    }
}
