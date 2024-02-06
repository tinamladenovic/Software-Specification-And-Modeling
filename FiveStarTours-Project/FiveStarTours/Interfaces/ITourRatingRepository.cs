using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveStarTours.Model;
using FiveStarTours.Repository;

namespace FiveStarTours.Interfaces
{
    public interface ITourRatingRepository
    {
        public List<TourRating> GetAll();
        public TourRating Save(TourRating rating);
        public int NextId();
        public TourRating GetById(int id);
        public TourRating Update(TourRating rating);
        public List<TourRating> GetAllByTour(int idTour, List<Attendance> attendances, List<KeyPoints> keyPointsRepository);
        public void Replace(TourRating rating);
    }
}
