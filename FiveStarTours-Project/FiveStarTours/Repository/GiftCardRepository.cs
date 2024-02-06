using FiveStarTours.Interfaces;
using FiveStarTours.Model;
using FiveStarTours.Serializer;
using FiveStarTours.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveStarTours.Repository
{
    public class GiftCardRepository : IGiftCardRepository
    {
        private const string FilePath = "../../../Resources/Data/giftcard.csv";

        private readonly Serializer<GiftCard> _serializer;

        private List<GiftCard> _giftCards;

        public GiftCardRepository()
        {
            _serializer = new Serializer<GiftCard>();
            _giftCards = _serializer.FromCSV(FilePath);
        }

        public List<GiftCard> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public GiftCard GetById(int id)
        {
            _giftCards = GetAll();
            foreach (GiftCard giftCard in _giftCards)
            {
                if (giftCard.Id == id)
                {
                    return giftCard;
                }
            }
            return null;
        }

        public List<GiftCard> GetAllById(int id)
        {
            _giftCards = GetAll();
            List<GiftCard> result = new List<GiftCard>();
            foreach (GiftCard card in _giftCards)
            {
                if (card.UserId == id)
                {
                    result.Add(card);
                }
            }
            return result;
        }
        public List<DateTime> GetAllDatesById(int id)
        {
            _giftCards = GetAll();
            List<DateTime> result = new List<DateTime>();
            foreach (GiftCard card in _giftCards)
            {
                if (card.UserId == id)
                {
                    result.Add(card.ExpiringDate);
                }
            }
            return result;
        }

        public int NextId()
        {
            _giftCards = _serializer.FromCSV(FilePath);
            if (_giftCards.Count < 1)
            {
                return 1;
            }
            return _giftCards.Max(gc => gc.Id) + 1;
        }
        public GiftCard Save(GiftCard giftCard)
        {
            giftCard.Id = NextId();
            _giftCards = _serializer.FromCSV(FilePath);
            _giftCards.Add(giftCard);
            _serializer.ToCSV(FilePath, _giftCards);
            return giftCard;
        }
        public void Delete(string giftCard)
        {
            _giftCards = GetAll();
            GiftCard founded = new GiftCard();
            foreach(var gc in _giftCards)
            {
                if(Convert.ToDateTime(giftCard) == gc.ExpiringDate)
                {
                    founded = gc;
                }
            }
            _giftCards.Remove(founded);
            _serializer.ToCSV(FilePath, _giftCards);
        }
    }
}
