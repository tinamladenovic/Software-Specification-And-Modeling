using System;
using System.Collections.Generic;
using System.Linq;
using FiveStarTours.Interfaces;
using FiveStarTours.Model;

namespace FiveStarTours.Services
{
    public class TourRequestService
    {
        private ITourRequestRepository _tourRequestRepository;
        public TourRequestService()
        {
            _tourRequestRepository = Injector.Injector.CreateInstance<ITourRequestRepository>();
        }

        public List<TourRequest> GetAll()
        {
            return _tourRequestRepository.GetAll();
        }

        public TourRequest Save(TourRequest tourRequest)
        {
            return _tourRequestRepository.Save(tourRequest);
        }

        public int NextId()
        {
            return _tourRequestRepository.NextId();
        }

        public TourRequest GetById(int id)
        {
            return _tourRequestRepository.GetById(id);
        }

        public List<TourRequest> GetAllProcessing()
        {
            List<TourRequest> list = new List<TourRequest>();
            foreach (TourRequest tourRequest in _tourRequestRepository.GetAll())
            {
                if(tourRequest.Status == Model.Enums.ReservationChangeStatusType.Processing)
                {
                    list.Add(tourRequest);
                }
            }

            return list;
        }

        public List<TourRequest> SearchRequest(Location location, int guestsNumber, Language language, DateInterval interval)
        {
            List<TourRequest> result = new List<TourRequest>();

            foreach (var request in _tourRequestRepository.GetAll())
            {
                if (IsMatch(location, guestsNumber, language, interval, request))
                {
                    result.Add(request);
                }
            }

            return result;
        }

        private bool IsMatch(Location location, int guestsNumber, Language language, DateInterval interval, TourRequest request)
        {
            bool locationMatch = location == null || location.State == null || SearchConditionLocation(request.Location, location);
            bool guestsNumberMatch = guestsNumber == 0 || SearchConditionGuestsNumber(request.MaxGuests, guestsNumber);
            bool languageMatch = language == null || SearchConditionLanguage(request.Language, language);
            bool intervalMatch = (interval.Start.Date == DateTime.Today && interval.End.Date == DateTime.Today) || SearchConditionInterval(request.Intervals, interval);

            return locationMatch && guestsNumberMatch && languageMatch && intervalMatch;
        }

        private bool SearchConditionLocation(Location location, Location searchLocation)
        {
            return location.City.Equals(searchLocation.City, StringComparison.OrdinalIgnoreCase);
        }

        private bool SearchConditionGuestsNumber(int number, int searchNumber)
        {
            return number == searchNumber;
        }

        private bool SearchConditionLanguage(Language language, Language searchLanguage)
        {
            return language.Name.Equals(searchLanguage.Name, StringComparison.OrdinalIgnoreCase);
        }

        private bool SearchConditionInterval(DateInterval dateInterval, DateInterval searchDateInterval)
        {
            return dateInterval.Start <= searchDateInterval.End && searchDateInterval.Start <= dateInterval.End;
        }


        public string GetByYear(string year, Location location, string Language)
        {
            if (location.City == null)
            {
                location.City = " ";
            }

            if (Language == null)
            {
                Language = " ";
            }

            int number = 0;
            foreach (var request in _tourRequestRepository.GetAll())
            {
                if (location.City == request.Location.City || Language.ToLower() == request.Language.Name.ToLower())
                {

                    if (request.Intervals.Start.Year.ToString() == year || request.Intervals.End.Year.ToString() == year)
                    {
                        number++;
                    }
                }
            }

            return number.ToString();
        }

        public string GetByMonth(string Month, int Year, Location location, string Language)
        {

            List<TourRequest> requests = _tourRequestRepository.GetAll();
            int number = 0;

            foreach (var request in requests)
            {
                if (location.City != null)
                {
                    if (request.Location.City != location.City)
                    {
                        continue;
                    }
                }

                if (Language != null && Language != "")
                {
                    if (request.Language.Name.ToLower() != Language.ToLower())
                    {
                        continue;
                    }
                }


                if ((request.Intervals.Start.Year == Year || request.Intervals.End.Year == Year) && (Convert.ToInt32(Month) == request.Intervals.Start.Month || Convert.ToInt32(Month) == request.Intervals.End.Month))
                {
                    number++;
                }

            }

            return number.ToString();
        }

        public Location GetMostWantedLocation()
        {
            List<TourRequest> items = _tourRequestRepository.GetAll();
            var groupedItems = items.GroupBy(item => item.Location.City, StringComparer.OrdinalIgnoreCase)
                        .OrderByDescending(group => group.Count())
                        .ToList();
            var mostRepeatedLocation = groupedItems.FirstOrDefault()?.Key;
            LocationsService _locationsService = new LocationsService();
            foreach (var location in _locationsService.GetAll())
            {
                if (mostRepeatedLocation == location.City)
                {
                    return location;
                }
            }

            return null;
        }

        public Language GetMostWantedLanguage()
        {
            List<TourRequest> items = _tourRequestRepository.GetAll();
            var groupedItems = items.GroupBy(item => item.Language.Name, StringComparer.OrdinalIgnoreCase)
                        .OrderByDescending(group => group.Count())
                        .ToList();
            var mostRepeatedLanguage = groupedItems.FirstOrDefault()?.Key;
            if (mostRepeatedLanguage == null)
            {
                return null;
            }
            else
            {
                Language language = new Language(mostRepeatedLanguage);
                return language;
            }
        }

        public void SaveChanges(TourRequest request)
        {
            foreach(var item in _tourRequestRepository.GetAll())
            {
                if(item.Intervals.Start == request.Intervals.Start && item.Language.Id == request.Language.Id && item.Location.Id == request.Location.Id)
                {
                    item.Status = request.Status;
                    item.AcceptDateTime = request.AcceptDateTime;
                    _tourRequestRepository.FindIdAndSave(item, item.Id);
                    return;
                }
            }

        }
    }
}
