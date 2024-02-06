using FiveStarTours.Interfaces;
using FiveStarTours.Model;
using FiveStarTours.Serializer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FiveStarTours.Repository
{
    public class AccommodationReservationsRepository : IAccommodationReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/accommondationReservations.csv";

        private readonly Serializer<AccommodationReservation> _serializer;

        private List<AccommodationReservation> _reservations;

        private static AccommodationReservationsRepository instance = null;
        private AccommodationRatingRepository _ratingRepository;
        private RecommodationRepository _recommodationRepository;
        private AccommodationsRepository _accommodationsRepository;
        private CancelationNotificationRepository _cancelationNotificationRepository;
        private UserRepository _userRepository;

        public AccommodationReservationsRepository()
        {
            _serializer = new Serializer<AccommodationReservation>();
            _reservations = _serializer.FromCSV(FilePath);
            _ratingRepository = AccommodationRatingRepository.GetInstace();
            _accommodationsRepository = new AccommodationsRepository();
            _recommodationRepository=RecommodationRepository.GetInstace();
            _cancelationNotificationRepository = new CancelationNotificationRepository();
            _userRepository = new UserRepository();
        }

        public static AccommodationReservationsRepository GetInstace()
        {
            if (instance == null)
            {
                instance = new AccommodationReservationsRepository();
            }
            return instance;
        }

        public List<AccommodationReservation> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public AccommodationReservation Save(AccommodationReservation reservation)
        {
            reservation.Id = NextId();
            _reservations = _serializer.FromCSV(FilePath);
            _reservations.Add(reservation);
            _serializer.ToCSV(FilePath, _reservations);
            return reservation;
        }

        public List<AccommodationReservation> Load()
        {
            return _serializer.FromCSV(FilePath);
        }

        public int NextId()
        {
            _reservations = _serializer.FromCSV(FilePath);
            if (_reservations.Count < 1)
            {
                return 1;
            }
            return _reservations.Max(t => t.Id) + 1;
        }

        public AccommodationReservation GetById(int id)
        {
            _reservations = GetAll();
            foreach (AccommodationReservation accommodationReservation in _reservations)
            {
                if (accommodationReservation.Id == id)
                {
                    return accommodationReservation;
                }
            }
            return null;
        }
        public void Delete(AccommodationReservation reservation)
        {
            _reservations = _serializer.FromCSV(FilePath);
            AccommodationReservation founded = _reservations.Find(c => c.Id == reservation.Id);
            _reservations.Remove(founded);
            _serializer.ToCSV(FilePath, _reservations);
        }

        public AccommodationReservation Update(AccommodationReservation reservation)
        {
            _reservations = _serializer.FromCSV(FilePath);
            AccommodationReservation current = _reservations.Find(c => c.Id == reservation.Id);
            int index = _reservations.IndexOf(current);
            _reservations.Remove(current);
            _reservations.Insert(index, reservation);
            _serializer.ToCSV(FilePath, _reservations);
            return reservation;
        }


        //SHOW UNRATED GUESTS FOR OWNER TO RATE
        public List<AccommodationReservation> GetUnratedAndLessThanFiveDaysAgo()
        {
            List<AccommodationReservation> reservations = new List<AccommodationReservation>();
            DateTime now = DateTime.Now;
            foreach (AccommodationReservation accommodationReservation in _reservations)
            {
                DateTime end = accommodationReservation.EndDate;
                if (end < now)
                {
                    if (accommodationReservation.RatedByOwner == false && now.AddDays(-5) < end)
                    {
                        reservations.Add(accommodationReservation);
                    }
                }
            }
            return reservations;

        }

        //SHOW GUEST REVIEWS TO OWNER
        public List<AccommodationReservation> GetRatesForOwner()
        {
            List<AccommodationReservation> reservations = new List<AccommodationReservation>();
            reservations = GetRatedByGuest();

            foreach (AccommodationReservation accommodationReservation in _reservations)
            {
                if (accommodationReservation.RatedByOwner == false)
                {
                    reservations.Remove(accommodationReservation);
                }
            }
            return reservations;
        }

        public List<AccommodationReservation> GetRatedByGuest()
        {
            List<AccommodationReservation> reservations = new List<AccommodationReservation>();
            reservations = GetAll();

            foreach (AccommodationReservation accommodationReservation in _reservations)
            {
                if (accommodationReservation.RatedByGuest == false)
                {
                    reservations.Remove(accommodationReservation);
                }
            }
            return reservations;
        }



        //NOTIFICATION FOR OWNER ABOUT UNRATED GUESTS
        public int CountUnrated()
        {
            int unrated = 0;
            _reservations = GetUnratedAndLessThanFiveDaysAgo();

            foreach (AccommodationReservation accommodationReservation in _reservations)
            {
                if (accommodationReservation.RatedByOwner == false)
                {
                    unrated++;
                }
            }
            return unrated;

        }

        public void NotifyAboutUnratedGuests()
        {
            _reservations = GetAll();
            int unrated = CountUnrated();
            if (unrated > 0)
            {
                MessageBox.Show("You have " + unrated + " forms that are waiting to be filled. Please fill them before they become unavailable!");
            }
            else
            {
                return;
            }

        }
        //END

        public bool DatesIntertwine(DateTime startAcc, DateTime endAcc, DateTime start, DateTime end)
        {
            bool isInInterval = false;
            if (start.Date <= endAcc.Date && end.Date >= startAcc.Date)
            {
                return true;
            }

            return isInInterval;
        }


        public List<AccommodationReservation> GetAllReservationsForAccommodationDateInterval(string accomodationName, DateTime start, DateTime end)
        {
            List<AccommodationReservation> accommodationReservations = new List<AccommodationReservation>();
            foreach (AccommodationReservation accommodationReservation in _reservations)
            {
                if (accomodationName == accommodationReservation.AccommodationName && DatesIntertwine(accommodationReservation.StartDate, accommodationReservation.EndDate, start, end))
                {
                    accommodationReservations.Add(accommodationReservation);

                }

            }
            return accommodationReservations;
        }

        public bool DoesInterwalIntertwineWithReservations(List<AccommodationReservation> reservations, DateTime start, DateTime end)
        {
            foreach (AccommodationReservation accommodationReservation in reservations)
            {
                if (DatesIntertwine(accommodationReservation.StartDate, accommodationReservation.EndDate, start, end))
                {
                    return true;
                }
            }

            return false;
        }



        public List<DateInterval> GetFreeDateIntervals(string accommodationName, DateTime start, DateTime end, int numberOfDays)
        {
            DateTime iterDate = start;
            List<AccommodationReservation> reservations = GetAllReservationsForAccommodationDateInterval(accommodationName, start, end);
            List<DateInterval> freeIntervals = new List<DateInterval>();

            while (iterDate.AddDays(numberOfDays).Date <= end.Date)
            {
                if (!DoesInterwalIntertwineWithReservations(reservations, iterDate, iterDate.AddDays(numberOfDays)))
                {
                    freeIntervals.Add(new DateInterval(iterDate, iterDate.AddDays(numberOfDays)));
                }

                iterDate = iterDate.AddDays(1);
            }


            return freeIntervals;
        }

        public bool IsAbleToRate(int reservationId)
        {
            DateTime now = DateTime.Now;

            if (_ratingRepository.ExistsRateForReservation(reservationId))
            {
                return false;
            }
            AccommodationReservation reservation = GetById(reservationId);
            if (reservation.EndDate < now && reservation.EndDate > now.AddDays(-5))
            {
                return true;
            }

            return false;
        }

        public bool IsAbleToCancel(int reservationId)
        {
            AccommodationReservation reservation = GetById(reservationId);
            DateTime now = DateTime.Now;
            Accommodation accommodation = _accommodationsRepository.GetAccommodationForAccommodationName(reservation.AccommodationName);
            if (reservation.StartDate > now.AddDays(1) && reservation.StartDate > now.AddDays(accommodation.DaysPossibleToCancel))
            {
                return true;
            }

            return false;
        }

        public void UserCancelsReservation(AccommodationReservation accommodationReservation)
        {
            int ownerId = 2;
            User guest = _userRepository.GetByNameSurname(accommodationReservation.GuestName);
            User owner = _userRepository.GetById(ownerId);
            CancelationNotification cancelationNotification = new CancelationNotification(-1, owner, guest, false);
            _cancelationNotificationRepository.Save(cancelationNotification);
        }
        public int CountReservations()
        {

            return GetAll().Count();

        }
        public int CountReservationsInLastYear()
        {
                DateTime lastYear = DateTime.Now.AddYears(-1);
                DateTime now = DateTime.Now;
                return GetAll().Where(r => r.StartDate > lastYear && r.StartDate < now).Count();
            
        }
        public int UpdateBonusPoints(int reservationCount, int currentPoints)
        {
            if (reservationCount >= 10)
            {
                return currentPoints + 5;
            }
            else
            {
                return currentPoints;
            }
        }


    }
}