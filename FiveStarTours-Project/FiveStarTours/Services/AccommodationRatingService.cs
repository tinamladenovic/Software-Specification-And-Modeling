using FiveStarTours.Interfaces;
using FiveStarTours.Model;
using FiveStarTours.Repository;
using FiveStarTours.Serializer;
using FiveStarTours.View.Traveler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FiveStarTours.Services
{
    public class AccommodationRatingService
    {
        private IAccommodationRatingRepository _accommodationRatingRepository;

        public AccommodationRatingService()
        {
            _accommodationRatingRepository = Injector.Injector.CreateInstance<IAccommodationRatingRepository>();
        }

        public List<AccommodationRating> GetAll()
        {
            return _accommodationRatingRepository.GetAll();
        }

        public AccommodationRating Save(AccommodationRating rating)
        {
            return _accommodationRatingRepository.Save(rating);
        }

        public int NextId()
        {
            return NextId();
        }

        public AccommodationRating GetById(int id)
        {
            return _accommodationRatingRepository.GetById(id);
        }

        public AccommodationRating Update(AccommodationRating rating)
        {
            return _accommodationRatingRepository.Update(rating);
        }

        //NUMBER OF RATINGS
        public int CountRatings()
        {
            return _accommodationRatingRepository.CountRatings();

        }

        //AVERAGE RATING
        public double AverageOwnerRating()
        {
            return _accommodationRatingRepository.AverageOwnerRating();

        }

        public bool ExistsRateForReservation(int reservationId)
        {
            return _accommodationRatingRepository.ExistsRateForReservation(reservationId);
        }
    }
}
