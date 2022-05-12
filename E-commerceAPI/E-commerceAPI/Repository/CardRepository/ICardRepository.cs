using E_commerceAPI.Model;
using System.Collections.Generic;

namespace E_commerceAPI.Repository.CardRepository
{
    public interface ICardRepository:IRepository<Card>
    {
        List<Card> GetItemsByUserID(string ID);
    }
}
