using FiveStarTours.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveStarTours.Interfaces
{
    public interface IAccommodationRatingRepository
    {
        List<AccommodationRating> GetAll();
        AccommodationRating Save(AccommodationRating rating);
        int NextId();
        AccommodationRating GetById(int id);
        AccommodationRating Update(AccommodationRating rating);
        int CountRatings();
        double AverageOwnerRating();
        bool ExistsRateForReservation(int reservationId);
    }
}
