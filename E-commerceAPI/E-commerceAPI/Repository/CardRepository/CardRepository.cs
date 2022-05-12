using E_commerceAPI.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace E_commerceAPI.Repository.CardRepository
{
    public class CardRepository : ICardRepository
    {
        private ContextDB contextDB;

        public CardRepository(ContextDB _contextDB)
        {
            contextDB = _contextDB;
        }
        public int Delete(int id)
        {
            contextDB.Cards.Remove(contextDB.Cards.Find(id));
            return contextDB.SaveChanges();
        }

        public List<Card> GetAll()
        {
            return contextDB.Cards.Include(c => c.ProductQuantity).ToList();
        }

        public Card GetById(int id)
        {
            return contextDB.Cards.Include(c => c.ProductQuantity).FirstOrDefault(c => c.ID == id);
        }

        public List<Card> GetItemsByUserID(string ID)
        {
            return contextDB.Cards.Include(c=>c.ProductQuantity).Where(c => c.UserID == ID).ToList();
        }

        public List<ProductQuantity> GetProducts(int id)
        {
            return contextDB.ProductQuantities.Include(p=>p.Product).Where(p => p.ID == id).ToList();
        }
        public int Insert(Card entity)
        {
            contextDB.Cards.Add(entity);
            return contextDB.SaveChanges();
        }

        public int Update(int id, Card entity)
        {
            var card = contextDB.Cards.FirstOrDefault(c => c.ID == id);
            if (card != null)
            {
                card.TotalPrice = entity.TotalPrice;
                card.ProductQuantity = entity.ProductQuantity;
            }
            return contextDB.SaveChanges();
        }

    }
}
