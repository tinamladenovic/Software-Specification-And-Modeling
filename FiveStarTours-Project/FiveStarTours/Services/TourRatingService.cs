using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveStarTours.Interfaces;
using FiveStarTours.Model;
using FiveStarTours.Repository;

namespace FiveStarTours.Services
{
    public class TourRatingService
    {
        private ITourRatingRepository _tourRatingRepository;
        public TourRatingService()
        {
            _tourRatingRepository = Injector.Injector.CreateInstance<ITourRatingRepository>();
        }

        public List<TourRating> GetAll()
        {
            return _tourRatingRepository.GetAll();
        }

        public TourRating Save(TourRating rating)
        {
            return _tourRatingRepository.Save(rating); 
        }

        public int NextId()
        {
            return _tourRatingRepository.NextId();
        }

        public TourRating GetById(int id)
        {
            return _tourRatingRepository.GetById(id);
        }

        public TourRating Update(TourRating rating)
        {
            return _tourRatingRepository.Update(rating);
        }

        public List<TourRating> GetAllByTour(int idTour, List<Attendance> attendances, List<KeyPoints> keyPointsRepository)
        {
            return _tourRatingRepository.GetAllByTour(idTour, attendances, keyPointsRepository);
        }

        public void Replace(TourRating rating)
        {
            _tourRatingRepository.Replace(rating);
        }
    }
}
