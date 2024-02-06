using FiveStarTours.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using FiveStarTours.Serializer;
using System.Xml.Linq;
using FiveStarTours.Interfaces;

namespace FiveStarTours.Repository
{
    public class AccommodationsRepository : IAccommodationsRepository
    {
        private const string FilePath = "../../../Resources/Data/accommodations.csv";

        private readonly Serializer<Accommodation> _serializer;

        private List<Accommodation> _accommodations;

        public AccommodationsRepository()
        {
            _serializer = new Serializer<Accommodation>();
            _accommodations = GetAllAccomodationBindLocation();
        }

        public List<Accommodation> GetAll()
        {
            return GetAllAccomodationBindLocation();
        }
        public List<Accommodation> GetAllAccomodationBindLocation()
        {
            LocationsRepository locationsRepository = new LocationsRepository();
            _accommodations = _serializer.FromCSV(FilePath);
            foreach(Accommodation accommodation in _accommodations)
            {
                int locId = accommodation.Location.Id;
                accommodation.Location = locationsRepository.GetById(locId);
            }
            return _accommodations;
        }

        public Accommodation Save(Accommodation accommodation)
        {
            accommodation.Id = NextId();
            _accommodations = GetAllAccomodationBindLocation();
            _accommodations.Add(accommodation);
            _serializer.ToCSV(FilePath, _accommodations);
            return accommodation;
        }



        public int NextId()
        {
            _accommodations = _serializer.FromCSV(FilePath);
            if (_accommodations.Count < 1)
            {
                return 1;
            }
            return _accommodations.Max(t => t.Id) + 1;
        }

        public void Delete(Accommodation accommodation)
        {
            _accommodations = GetAllAccomodationBindLocation();
            Accommodation founded = _accommodations.Find(c => c.Id == accommodation.Id);
            _accommodations.Remove(founded);
            _serializer.ToCSV(FilePath, _accommodations);
        }

        public Accommodation Update(Accommodation accommodation)
        {
            _accommodations = GetAllAccomodationBindLocation();
            Accommodation current = _accommodations.Find(c => c.Id == accommodation.Id);
            int index = _accommodations.IndexOf(current);
            _accommodations.Remove(current);
            _accommodations.Insert(index, accommodation);    
            _serializer.ToCSV(FilePath, _accommodations);
            return accommodation;
        }

        public Accommodation GetAccommodationForReservation(AccommodationReservation accommodationReservation)
        {
            foreach(Accommodation accommodation in _accommodations)
            {
                if(accommodationReservation.AccommodationName == accommodation.AccommodationName)
                {
                    return accommodation;
                }
            }
            return null;
        }


        public bool SearchConditionAccommodationName(Accommodation accommodation, string name)
        {
                return accommodation.AccommodationName.ToLower().Contains(name.ToLower());
        }

        public bool SearchConditionAccommodationLocation(Accommodation accommodation, string state, string city)
        {
            return accommodation.Location.State.ToLower().Contains(state.ToLower()) && accommodation.Location.City.ToLower().Contains(city.ToLower());
        }

        public bool SearchConditionAccommodationType(Accommodation accommodation, string type)
        {
            return accommodation.Type.ToString().ToLower().Contains(type.ToLower());   
        }
        
        public bool SearchConditionNumberOfGuest(Accommodation accommodation, string maxGuestNum)
        {
            if(maxGuestNum == null || maxGuestNum == "")
            {
                return true;
            }
            int maxGuests = Convert.ToInt32(maxGuestNum);
            return accommodation.MaxGuestNum >=maxGuests;
        }

       public bool SearchConditionReservationDays(Accommodation accommodation, string minReservationDays)
        {
            if(minReservationDays == null || minReservationDays == "")
            {
                return true;
            }
            int minReserve = Convert.ToInt32(minReservationDays);
            return accommodation.MinReservationDays <= minReserve;
        }

        public List<Accommodation> SearchAccomodations(string name, string state, string city, string maxGuestNum ,string type, string minReservationDays)
        {
            List<Accommodation> searchedAccommodations = new List<Accommodation>();
            foreach(Accommodation accommodation in _accommodations)
            {
                if (!SearchConditionAccommodationName(accommodation, name))
                {
                    continue;
                }
                if(!SearchConditionAccommodationLocation(accommodation, state, city))
                {
                    continue;
                }
                if(!SearchConditionNumberOfGuest(accommodation, maxGuestNum))
                {
                    continue;
                }
                if(!SearchConditionAccommodationType(accommodation,type))
                {
                    continue;
                }
                if(!SearchConditionReservationDays(accommodation,minReservationDays))
                {
                    continue;
                }
                searchedAccommodations.Add(accommodation);
            }
            return searchedAccommodations;
        }



        public Accommodation GetAccommodationForAccommodationName(string accommodationName)
        {
            foreach (Accommodation accommodation in _accommodations)
            {
                if (accommodationName == accommodation.AccommodationName)
                {
                    return accommodation;
                }
            }
            return null;
        }

        public bool CheckReservationDays(Accommodation accommodation, string minDays)
        {
            if (minDays == null || minDays == "")
            {
                return true;
            }
            int minReserve = Convert.ToInt32(minDays);
            return accommodation.MinReservationDays <= minReserve;
        }

        public List<string> MakeUrlsList(string urls)
        {
            List<string> result = new List<string>();

            Array _urls = urls.Split(", ");

            foreach (string url in _urls)
            {
                result.Add(url);
            }

            return result;
        }
       


    }
}
