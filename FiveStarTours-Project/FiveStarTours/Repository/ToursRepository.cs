// Modeled on CommentRepository from InitialProject

using System;
using System.Collections.Generic;
using System.Linq;
using FiveStarTours.Interfaces;
using FiveStarTours.Model;
using FiveStarTours.Serializer;

namespace FiveStarTours.Repository
{
    public class ToursRepository : IToursRepository
    {
        private const string FilePath = "../../../Resources/Data/tours.csv";

        private readonly Serializer<Tour> _serializer;

        private List<Tour> _tours;

        public ToursRepository()
        {
            _serializer = new Serializer<Tour>();
            _tours = _serializer.FromCSV(FilePath);
        }

        public List<Tour> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Tour Save(Tour tour)
        {
            tour.Id = NextId();
            _tours = _serializer.FromCSV(FilePath);
            _tours.Add(tour);
            _serializer.ToCSV(FilePath, _tours);
            return tour;
        }

        public int NextId()
        {
            _tours = _serializer.FromCSV(FilePath);
            if (_tours.Count < 1)
            {
                return 1;
            }
            return _tours.Max(t => t.Id) + 1;
        }

        public Tour GetById(int id)
        {
            _tours = GetAll();
            foreach (Tour tour in _tours)
            {
                if(tour.Id == id)
                {
                    return tour;
                }
            }
            return null;
        }

        public List<Tour> GetAllByDate(DateTime date, User user)
        {
            List<Tour> toursByDate = new List<Tour>();
            var tours = GetByUser(user);
            foreach (var tour in tours)
            {
                foreach (DateTime dateTime in tour.Beginning)
                {
                    if (dateTime.Date == date.Date)
                    {
                        DateTime tourDate = dateTime.Date;
                        TimeSpan tourTime = dateTime.TimeOfDay;

                        DateTime newDate = new DateTime(tourDate.Year, tourDate.Month, tourDate.Day, tourTime.Hours, tourTime.Minutes, tourTime.Seconds);
                        Location location = tour.getLocationById(tour.IdLocation);
                        Tour newTour = new Tour(tour.Name,tour.User, tour.IdLocation, location, tour.Description, tour.IdLanguages, tour.Languages, tour.MaxGuests, tour.IdKeyPoints, tour.KeyPoints, newDate, tour.Duration, tour.ImageUrls);
                        toursByDate.Add(newTour);

                    }
                }
            }

            return toursByDate;
        }

        public List<Tour> GetByUser(User user)
        {
            _tours = _serializer.FromCSV(FilePath);
            return _tours.FindAll(c => c.User.Id == user.Id);
        }

        public void DeleteByDate(Tour tour)
        {
            _tours = _serializer.FromCSV(FilePath);
            Tour founded = _tours.Find(t => t.Id == tour.Id);
            foreach(DateTime date in founded.Beginning)
            {
                if(date == tour.OneBeginningTime)
                {
                    founded.Beginning.Remove(date);
                    if (founded.Beginning.Count == 0)
                    {
                        _tours.Remove(founded);
                    }
                    break;
                }
                
            }
            _serializer.ToCSV(FilePath, _tours);
        }

        public List<string> GetNamesById(List<int> ids)
        {
            List<string> result = new List<string>();
            _tours = GetAll();
            foreach (int id in ids)
            {
                foreach(var tour in _tours)
                {
                    if(tour.Id == id)
                    {
                        result.Add(tour.Name);
                    }
                }
            }
            return result;
        }

        public int GetByName(string name)
        {
            _tours = GetAll();
            int result = 0;
            foreach(var tour in _tours)
            {
                if(tour.Name == name)
                {
                    result = tour.Id;
                }
            }
            return result;
        }
    }
}
