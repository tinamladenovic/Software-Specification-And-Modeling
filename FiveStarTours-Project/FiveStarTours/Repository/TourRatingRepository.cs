using System.Collections.Generic;
using System.Linq;
using FiveStarTours.Interfaces;
using FiveStarTours.Model;
using FiveStarTours.Serializer;

namespace FiveStarTours.Repository
{
    public class TourRatingRepository : ITourRatingRepository
    {
        private const string FilePath = "../../../Resources/Data/tourRating.csv";

        private readonly Serializer<TourRating> _serializer;

        private List<TourRating> _tourRatingRepository;

        public TourRatingRepository()
        {
            _serializer = new Serializer<TourRating>();
            _tourRatingRepository = _serializer.FromCSV(FilePath);

        }
        public List<TourRating> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public TourRating Save(TourRating rating)
        {
            rating.Id = NextId();
            _tourRatingRepository = _serializer.FromCSV(FilePath);
            _tourRatingRepository.Add(rating);
            _serializer.ToCSV(FilePath, _tourRatingRepository);
            return rating;
        }

        public int NextId()
        {
            _tourRatingRepository = _serializer.FromCSV(FilePath);
            if (_tourRatingRepository.Count < 1)
            {
                return 1;
            }
            return _tourRatingRepository.Max(t => t.Id) + 1;
        }

        public TourRating GetById(int id)
        {
            _tourRatingRepository = GetAll();
            foreach (TourRating rating in _tourRatingRepository)
            {
                if (rating.Id == id)
                {
                    return rating;
                }
            }
            return null;
        }

        public TourRating Update(TourRating rating)
        {
            _tourRatingRepository = _serializer.FromCSV(FilePath);
            TourRating current = _tourRatingRepository.Find(c => c.Id == rating.Id);
            int index = _tourRatingRepository.IndexOf(current);
            _tourRatingRepository.Remove(current);
            _tourRatingRepository.Insert(index, rating);
            _serializer.ToCSV(FilePath, _tourRatingRepository);
            return rating;
        }

        public List<TourRating> GetAllByTour(int idTour, List<Attendance> attendances, List<KeyPoints> keyPointsRepository)
        {
            _tourRatingRepository = GetAll();
            List<TourRating> result = new List<TourRating>();
            foreach (TourRating rating in _tourRatingRepository)
            {
                if (rating.TourId == idTour)
                {
                    foreach(Attendance attendance in attendances)
                    {
                        if(rating.UserId == attendance.IdVisitor && attendance.IdTour == idTour)
                        {
                            foreach(KeyPoints keyPoint in keyPointsRepository)
                            {
                                if(attendance.IdKeyPoint == keyPoint.Id)
                                {
                                    rating.KeyPoint = keyPoint;
                                    result.Add(rating);
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }

        public void Replace(TourRating rating)
        {
            _tourRatingRepository = _serializer.FromCSV(FilePath);
            foreach (TourRating r in _tourRatingRepository)
            {
                if(r.Id == rating.Id)
                {
                    r.Reported = true;
                }
            }
            _serializer.ToCSV(FilePath, _tourRatingRepository);
        }
    }
}
