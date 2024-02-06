using FiveStarTours.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveStarTours.Interfaces
{
    public interface IAccommodationReservationRepository
    {
        List<AccommodationReservation> GetAll();
        AccommodationReservation Save(AccommodationReservation reservation);

        List<AccommodationReservation> Load();
        int NextId();
        AccommodationReservation GetById(int id);
        void Delete(AccommodationReservation reservation);
        AccommodationReservation Update(AccommodationReservation reservation);
        List<AccommodationReservation> GetUnratedAndLessThanFiveDaysAgo();
        List<AccommodationReservation> GetRatesForOwner();
        List<AccommodationReservation> GetRatedByGuest();
        int CountUnrated();
        void NotifyAboutUnratedGuests();
        bool DatesIntertwine(DateTime startAcc, DateTime endAcc, DateTime start, DateTime end);
        List<AccommodationReservation> GetAllReservationsForAccommodationDateInterval(string accomodationName, DateTime start, DateTime end);
        bool DoesInterwalIntertwineWithReservations(List<AccommodationReservation> reservations, DateTime start, DateTime end);
        List<DateInterval> GetFreeDateIntervals(string accommodationName, DateTime start, DateTime end, int numberOfDays);
        bool IsAbleToRate(int reservationId);
        bool IsAbleToCancel(int reservationId);
        void UserCancelsReservation(AccommodationReservation accommodationReservation);
        int CountReservations();
        int CountReservationsInLastYear();
        int UpdateBonusPoints(int reservationCount, int currentPoints);


    }
}
