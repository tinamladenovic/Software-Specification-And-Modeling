using FiveStarTours.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveStarTours.Interfaces
{
    public interface IGiftCardRepository
    {
        List<GiftCard> GetAll();
        GiftCard GetById(int id);
        List<GiftCard> GetAllById(int id);
        List<DateTime> GetAllDatesById(int id);
        int NextId();
        GiftCard Save(GiftCard giftCard);
        void Delete(string giftCard);
    }
}
