using FiveStarTours.Interfaces;
using FiveStarTours.Model;
using FiveStarTours.Repository;
using FiveStarTours.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FiveStarTours.Services
{
    public class AccommodationsService
    {
        private IAccommodationsRepository _accommodationsRepository;

        public AccommodationsService()
        {
            _accommodationsRepository = Injector.Injector.CreateInstance<IAccommodationsRepository>();

        }


        public List<Accommodation> GetAll()
        {
            return _accommodationsRepository.GetAll();
        }
        public List<Accommodation> GetAllAccomodationBindLocation()
        {
            return _accommodationsRepository.GetAllAccomodationBindLocation();
        }

        public Accommodation Save(Accommodation accommodation)
        {
            return _accommodationsRepository.Save(accommodation);
        }



        public int NextId()
        {
            return NextId();
        }

        public void Delete(Accommodation accommodation)
        {
            _accommodationsRepository.Delete(accommodation);
        }

        public Accommodation Update(Accommodation accommodation)
        {
            return _accommodationsRepository.Update(accommodation);
        }

        public Accommodation GetAccommodationForReservation(AccommodationReservation accommodationReservation)
        {
            return _accommodationsRepository.GetAccommodationForReservation(accommodationReservation);
        }


        public bool SearchConditionAccommodationName(Accommodation accommodation, string name)
        {
            return _accommodationsRepository.SearchConditionAccommodationName(accommodation, name);
        }

        public bool SearchConditionAccommodationLocation(Accommodation accommodation, string state, string city)
        {
            return _accommodationsRepository.SearchConditionAccommodationLocation(accommodation, state, city);
        }

        public bool SearchConditionAccommodationType(Accommodation accommodation, string type)
        {
            return _accommodationsRepository.SearchConditionAccommodationType(accommodation, type);
        }

        public bool SearchConditionNumberOfGuest(Accommodation accommodation, string maxGuestNum)
        {
            return _accommodationsRepository.SearchConditionNumberOfGuest(accommodation, maxGuestNum);
        }

        public bool SearchConditionReservationDays(Accommodation accommodation, string minReservationDays)
        {
            return _accommodationsRepository.SearchConditionReservationDays( accommodation,  minReservationDays);
        }

        public List<Accommodation> SearchAccomodations(string name, string state, string city, string maxGuestNum, string type, string minReservationDays)
        {
            return _accommodationsRepository.SearchAccomodations(name, state, city, maxGuestNum, type, minReservationDays);
        }



        public Accommodation GetAccommodationForAccommodationName(string accommodationName)
        {
            return _accommodationsRepository.GetAccommodationForAccommodationName(accommodationName);
        }

        public bool CheckReservationDays(Accommodation accommodation, string minDays)
        {
            return _accommodationsRepository.CheckReservationDays(accommodation, minDays);
        }


    }
}
