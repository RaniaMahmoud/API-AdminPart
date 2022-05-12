using E_commerceAPI.Model;

namespace E_commerceAPI.DTO
{
    public class CardDTO
    {
        public int ID { get; set; }
        public double TotalPrice { get; set; }
        public string UserID { get; set; }        
        public ProductQuantity ProductQuantity { get; set; }
    }
}
