using FiveStarTours.Interfaces;
using FiveStarTours.Model;
using FiveStarTours.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveStarTours.Repository
{
    public class TourRequestRepository : ITourRequestRepository
    {
        private const string FilePath = "../../../Resources/Data/tourRequest.csv";

        private readonly Serializer<TourRequest> _serializer;

        private List<TourRequest> _tourRequest;

        public TourRequestRepository()
        {
            _serializer = new Serializer<TourRequest>();
            _tourRequest = _serializer.FromCSV(FilePath);
        }

        public List<TourRequest> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public TourRequest Save(TourRequest tourRequest)
        {
            tourRequest.Id = NextId();
            _tourRequest = _serializer.FromCSV(FilePath);
            _tourRequest.Add(tourRequest);
            _serializer.ToCSV(FilePath, _tourRequest);
            return tourRequest;
        }

        public int NextId()
        {
            _tourRequest = _serializer.FromCSV(FilePath);
            if (_tourRequest.Count < 1)
            {
                return 1;
            }
            return _tourRequest.Max(t => t.Id) + 1;
        }

        public TourRequest GetById(int id)
        {
            _tourRequest = GetAll();
            foreach (TourRequest tourRequest in _tourRequest)
            {
                if (tourRequest.Id == id)
                {
                    return tourRequest;
                }
            }
            return null;
        }

   
       
    }
}
