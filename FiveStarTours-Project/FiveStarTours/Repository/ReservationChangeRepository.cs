using FiveStarTours.Interfaces;
using FiveStarTours.Model;
using FiveStarTours.Serializer;
using FiveStarTours.View.Traveler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveStarTours.Repository
{
    public class ReservationChangeRepository : IReservationChangeRepository
    {
        private const string FilePath = "../../../Resources/Data/reservationChanges.csv";

        private readonly Serializer<ReservationChange> _serializer;

        private List<ReservationChange> _changes;

        private static ReservationChangeRepository instance = null;

        public static ReservationChangeRepository GetInstance()
        {
            if (instance == null)
            {
                instance = new ReservationChangeRepository();
            }
            return instance;
        }
        public ReservationChangeRepository()
        {
            _serializer = new Serializer<ReservationChange>();
            _changes = _serializer.FromCSV(FilePath);
            BindAccommodationReservation();
        }

        public List<ReservationChange> GetAll()
        {
            return _changes;
        }
        
        public List<ReservationChange> GetAllProcessing()
        {
            List<ReservationChange> changes = new List<ReservationChange>();

            foreach (ReservationChange change in _changes)
            {
                if (change.Status == Model.Enums.ReservationChangeStatusType.Processing)
                {
                    changes.Add(change);
                }
            }
            return changes;
        }

        public ReservationChange Save(ReservationChange changes)
        {
           changes.Id = NextId();
            _changes = _serializer.FromCSV(FilePath);
            _changes.Add(changes);
            _serializer.ToCSV(FilePath, _changes);
            return changes;
        }

        public void BindAccommodationReservation()
        {
            AccommodationReservationsRepository accommodationReservationsRepository = AccommodationReservationsRepository.GetInstace();
            foreach(ReservationChange change in _changes)
            {
                int id = change.AccommodationReservation.Id;
                AccommodationReservation accommodationReservation = accommodationReservationsRepository.GetById(id);
                change.AccommodationReservation = accommodationReservation;
            }
        }

        public int NextId()
        {
            if (_changes.Count < 1)
            {
                return 1;
            }
            return _changes.Max(t => t.Id) + 1;
        }

        public ReservationChange GetById(int id)
        {
            _changes = GetAll();
            foreach (ReservationChange changes in _changes)
            {
                if (changes.Id == id)
                {
                    return changes;
                }
            }
            return null;
        }

        public ReservationChange Update(ReservationChange changes)
        {
            ReservationChange current = _changes.Find(c => c.Id == changes.Id);
            int index = _changes.IndexOf(current);
            _changes.Remove(current);
            _changes.Insert(index, changes);
            _serializer.ToCSV(FilePath, _changes);
            return changes;
        }

        public bool IsBusy(ReservationChange changes)
        {
            bool newStartBeforeEnd = changes.NewStartDate <= changes.AccommodationReservation.EndDate;
            bool newEndAfterStart = changes.NewEndDate >= changes.AccommodationReservation.StartDate;
            bool anyDurationOverlap = newStartBeforeEnd && newEndAfterStart;

            return anyDurationOverlap;
        }
    }
}
