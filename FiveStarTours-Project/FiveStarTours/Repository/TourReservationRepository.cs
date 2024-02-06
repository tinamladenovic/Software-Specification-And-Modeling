using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using FiveStarTours.Interfaces;
using FiveStarTours.Model;
using FiveStarTours.Serializer;
using FiveStarTours.View;


namespace FiveStarTours.Repository
{
    public class TourReservationRepository : ITourReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/tourreservations.csv";
        private readonly Serializer<TourReservation> _serializerVisitor;

        private List<TourReservation> _tourReservations;
        public TourReservationRepository()
        {
            _serializerVisitor = new Serializer<TourReservation>();
            _tourReservations = _serializerVisitor.FromCSV(FilePath);
        }

        public List<TourReservation> GetAll()
        {
            return _serializerVisitor.FromCSV(FilePath);
        }

        public TourReservation Save(TourReservation tourReservation)
        {
            tourReservation.Id = NextId();
            _tourReservations = _serializerVisitor.FromCSV(FilePath);
            _tourReservations.Add(tourReservation);
            _serializerVisitor.ToCSV(FilePath, _tourReservations);
            return tourReservation;
        }

        public int NextId()
        {
            _tourReservations = _serializerVisitor.FromCSV(FilePath);
            if (_tourReservations.Count < 1)
            {
                return 1;
            }
            return _tourReservations.Max(t => t.Id) + 1;
        }

        public int ReservedSeats(Tour tour)
        {
            int reservedSeats = 0;
            List<TourReservation> tourReservations = new List<TourReservation>();
            tourReservations = GetAll();
            foreach (var tourReservation in _tourReservations)
            {
                foreach(var date in tour.Beginning)
                {
                    if(tourReservation.TourId == tour.Id && tourReservation.DateTime == date)
                    {
                        reservedSeats += tourReservation.MembersNumber;
                    }
                }
            }
            return reservedSeats;
        }

        public List<string> GetAllVisitors(Tour tour)
        {
            List<string> visitors = new List<string>();
            foreach(var tourReservation in _tourReservations)
            {
                if(tourReservation.TourId == tour.Id)
                {
                    foreach(var name in tourReservation.VisitorName)
                    {
                        visitors.Add(name);
                    }
                }
            }
            return visitors;
        }

        public int GetWithGiftCard(LiveTour tour, List<Attendance> attendances, List<User> users)
        {
            int result = 0;
            _tourReservations = GetAll();
            foreach (var tourReservation in _tourReservations)
            {
                if(tourReservation.TourId == tour.IdTour && tourReservation.DateTime == tour.Date && tourReservation.GiftCard)
                {
                    foreach(string name in tourReservation.VisitorName)
                    {
                        foreach(var user in users)
                        {
                            if(user.Name == name)
                            {
                                foreach(Attendance attendance in attendances)
                                {
                                    if(user.Id == attendance.IdVisitor)
                                    {
                                        result++;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }

        public void DeleteById(Tour tour)
        {
            List <TourReservation> tourReservations = GetAll();
            _tourReservations = _serializerVisitor.FromCSV(FilePath);
            foreach (TourReservation tourReservation in tourReservations)
            {
                if(tourReservation.TourId == tour.Id && tourReservation.DateTime == tour.OneBeginningTime)
                {
                    Delete(tourReservation);
                }
            }

            _serializerVisitor.ToCSV(FilePath, _tourReservations);
        }

        public void Delete(TourReservation tourReservation)
        {
            _tourReservations = GetAll();
            TourReservation founded = _tourReservations.Find(t => t.Id == tourReservation.Id);
            _tourReservations.Remove(founded);
            _serializerVisitor.ToCSV(FilePath, _tourReservations);
        }
    }
}
   
