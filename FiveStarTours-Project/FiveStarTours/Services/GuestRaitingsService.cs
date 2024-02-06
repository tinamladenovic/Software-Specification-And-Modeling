using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveStarTours.Interfaces;
using FiveStarTours.Model;

namespace FiveStarTours.Services
{
    public class GuestRaitingsService
    {
        private IGuestRatingsRepository _guestRatingRepository;

        public GuestRaitingsService()
        {
            _guestRatingRepository = Injector.Injector.CreateInstance<IGuestRatingsRepository>();
        }

        public List<GuestRating> GetAll()
        {
            return _guestRatingRepository.GetAll();
        }

        public GuestRating Save(GuestRating rating)
        {
            return _guestRatingRepository.Save(rating);
        }

        public int NextId()
        {
            return _guestRatingRepository.NextId();
        }

        public GuestRating GetById(int id)
        {
            return _guestRatingRepository.GetById(id);
        }

        public GuestRating Update(GuestRating rating)
        {
            return _guestRatingRepository.Update(rating);
        }
    }
}
