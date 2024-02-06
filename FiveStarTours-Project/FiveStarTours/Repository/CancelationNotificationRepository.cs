using FiveStarTours.Interfaces;
using FiveStarTours.Model;
using FiveStarTours.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FiveStarTours.Repository
{
    public class CancelationNotificationRepository : ICancelationNotificationRepository
    {
        private const string FilePath = "../../../Resources/Data/cancelationNotification.csv";

        private readonly Serializer<CancelationNotification> _serializer;
        private List<CancelationNotification> _cancelations;
        private List<AccommodationReservation> _reservations;



        public CancelationNotificationRepository()
        {
            _serializer = new Serializer<CancelationNotification>();
            _cancelations = _serializer.FromCSV(FilePath);
        }

        public List<CancelationNotification> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }
        public CancelationNotification Update(CancelationNotification cancelation)
        {
            _cancelations = _serializer.FromCSV(FilePath);
            CancelationNotification current = _cancelations.Find(c => c.Id == cancelation.Id);
            int index = _cancelations.IndexOf(current);
            _cancelations.Remove(current);
            _cancelations.Insert(index, cancelation);
            _serializer.ToCSV(FilePath, _cancelations);
            return cancelation;
        }
        public CancelationNotification Save(CancelationNotification cancelationNotification)
        {
            cancelationNotification.Id = NextId();
            _cancelations.Add(cancelationNotification);
            _serializer.ToCSV(FilePath, _cancelations);
            return cancelationNotification;
        }



        public int NextId()
        {
            _cancelations = _serializer.FromCSV(FilePath);
            if (_cancelations.Count < 1)
            {
                return 1;
            }
            return _cancelations.Max(t => t.Id) + 1;
        }
   
      


    }
}
