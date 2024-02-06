using FiveStarTours.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveStarTours.Interfaces
{
    public interface IAccommodationsRepository
    {
        List<Accommodation> GetAll();
        List<Accommodation> GetAllAccomodationBindLocation();
        Accommodation Save(Accommodation accommodation);
        int NextId();
        void Delete(Accommodation accommodation);
        Accommodation Update(Accommodation accommodation);
        Accommodation GetAccommodationForReservation(AccommodationReservation accommodationReservation);
        bool SearchConditionAccommodationName(Accommodation accommodation, string name);
        bool SearchConditionAccommodationLocation(Accommodation accommodation, string state, string city);
        bool SearchConditionAccommodationType(Accommodation accommodation, string type);
        bool SearchConditionNumberOfGuest(Accommodation accommodation, string maxGuestNum);
        bool SearchConditionReservationDays(Accommodation accommodation, string minReservationDays);
        List<Accommodation> SearchAccomodations(string name, string state, string city, string maxGuestNum, string type, string minReservationDays);
        Accommodation GetAccommodationForAccommodationName(string accommodationName);
        bool CheckReservationDays(Accommodation accommodation, string minDays);

    }
}
