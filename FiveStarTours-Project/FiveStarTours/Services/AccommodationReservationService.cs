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

namespace FiveStarTours.Services
{
    public class AccommodationReservationService
    {
        private IAccommodationReservationRepository _accommodationReservationRepository;

        public AccommodationReservationService()
        {
            _accommodationReservationRepository = Injector.Injector.CreateInstance<IAccommodationReservationRepository>();
        }

        public List<AccommodationReservation> GetAll()
        {
            return _accommodationReservationRepository.GetAll();
        }

        public AccommodationReservation Save(AccommodationReservation reservation)
        {
            return _accommodationReservationRepository.Save(reservation);
        }

        public List<AccommodationReservation> Load()
        {
            return _accommodationReservationRepository.Load();
        }

        public int NextId()
        {
            return NextId();
        }

        public AccommodationReservation GetById(int id)
        {
            return _accommodationReservationRepository.GetById(id);
        }
        public void Delete(AccommodationReservation reservation)
        {
            _accommodationReservationRepository.Delete(reservation);
        }

        public AccommodationReservation Update(AccommodationReservation reservation)
        {
            return _accommodationReservationRepository.Update(reservation);
        }


        //SHOW UNRATED GUESTS FOR OWNER TO RATE
        public List<AccommodationReservation> GetUnratedAndLessThanFiveDaysAgo()
        {
            return _accommodationReservationRepository.GetUnratedAndLessThanFiveDaysAgo();

        }


        //SHOW GUEST REVIEWS TO OWNER
        public List<AccommodationReservation> GetRatesForGuest()
        {
            return _accommodationReservationRepository.GetRatesForOwner();
        }

        public List<AccommodationReservation> GetRatedByGuest()
        {
            return _accommodationReservationRepository.GetRatedByGuest();
        }



        //NOTIFICATION FOR OWNER ABOUT UNRATED GUESTS
        public int CountUnrated()
        {
            return _accommodationReservationRepository.CountUnrated();

        }

        public void NotifyAboutUnratedGuests()
        {
            _accommodationReservationRepository.NotifyAboutUnratedGuests();

        }
        //END

        public bool DatesIntertwine(DateTime startAcc, DateTime endAcc, DateTime start, DateTime end)
        {
            return _accommodationReservationRepository.DatesIntertwine(startAcc, endAcc, start, end);
        }


        public List<AccommodationReservation> GetAllReservationsForAccommodationDateInterval(string accomodationName, DateTime start, DateTime end)
        {
            return _accommodationReservationRepository.GetAllReservationsForAccommodationDateInterval(accomodationName, start, end);
        }

        public bool DoesInterwalIntertwineWithReservations(List<AccommodationReservation> reservations, DateTime start, DateTime end)
        {
            return _accommodationReservationRepository.DoesInterwalIntertwineWithReservations(reservations, start, end);
        }



        public List<DateInterval> GetFreeDateIntervals(string accommodationName, DateTime start, DateTime end, int numberOfDays)
        {
            return _accommodationReservationRepository.GetFreeDateIntervals(accommodationName, start, end, numberOfDays);
        }

        public bool IsAbleToRate(int reservationId)
        {
            return _accommodationReservationRepository.IsAbleToRate(reservationId);
        }

        public bool IsAbleToCancel(int reservationId)
        {
            return _accommodationReservationRepository.IsAbleToCancel(reservationId);
        }

        public void UserCancelsReservation(AccommodationReservation accommodationReservation)
        {
            _accommodationReservationRepository.UserCancelsReservation(accommodationReservation);
        }

        public int CountReservations()
        {
            return _accommodationReservationRepository.CountReservations();

        }
        public int CountReservationsInLastYear()
        {
            return _accommodationReservationRepository.CountReservationsInLastYear();
        }
        public int UpdateBonusPoints(int reservationCount, int currentPoints) 
        {
            return _accommodationReservationRepository.UpdateBonusPoints( reservationCount,  currentPoints);
        }


    }

}
