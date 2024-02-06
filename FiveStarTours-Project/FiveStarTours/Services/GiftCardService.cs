using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveStarTours.Interfaces;
using FiveStarTours.Model;

namespace FiveStarTours.Services
{
    public class GiftCardService
    {
        private IGiftCardRepository _giftCardRepository;

        public GiftCardService()
        {
            _giftCardRepository = Injector.Injector.CreateInstance<IGiftCardRepository>();
        }

        public List<GiftCard> GetAll()
        {
            return _giftCardRepository.GetAll();
        }

        public GiftCard GetById(int id)
        {
            return _giftCardRepository.GetById(id);
        }

        public List<GiftCard> GetAllById(int id)
        {
            return _giftCardRepository.GetAllById(id);
        }
        public List<DateTime> GetAllDatesById(int id)
        {
            return _giftCardRepository.GetAllDatesById(id);
        }
        public int NextId()
        {
            return _giftCardRepository.NextId();
        }
        public GiftCard Save(GiftCard giftCard)
        {
            return _giftCardRepository.Save(giftCard);
        }
        public void Delete(string giftCard)
        {
            _giftCardRepository.Delete(giftCard);
        }
    }
}
