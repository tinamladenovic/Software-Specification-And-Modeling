using FiveStarTours.Interfaces;
using FiveStarTours.Model;
using FiveStarTours.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FiveStarTours.Repository
{
    public class GuestRatingsRepository : IGuestRatingsRepository
    {
        private const string FilePath = "../../../Resources/Data/guestRatings.csv";

        private readonly Serializer<GuestRating> _serializer;

        private List<GuestRating> _ratings;

        public GuestRatingsRepository()
        {
            _serializer = new Serializer<GuestRating>();
            _ratings = _serializer.FromCSV(FilePath);
        }

        public List<GuestRating> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public GuestRating Save(GuestRating rating)
        {
            rating.Id = NextId();
            _ratings = _serializer.FromCSV(FilePath);
            _ratings.Add(rating);
            _serializer.ToCSV(FilePath, _ratings);
            return rating;
        }

        public int NextId()
        {
            _ratings = _serializer.FromCSV(FilePath);
            if (_ratings.Count < 1)
            {
                return 1;
            }
            return _ratings.Max(t => t.Id) + 1;
        }

        public GuestRating GetById(int id)
        {
            _ratings = GetAll();
            foreach (GuestRating rating in _ratings)
            {
                if (rating.Id == id)
                {
                    return rating;
                }
            }
            return null;
        }

        public GuestRating Update(GuestRating rating)
        {
            _ratings = _serializer.FromCSV(FilePath);
            GuestRating current = _ratings.Find(c => c.Id == rating.Id);
            int index = _ratings.IndexOf(current);
            _ratings.Remove(current);
            _ratings.Insert(index, rating);  
            _serializer.ToCSV(FilePath, _ratings);
            return rating;
        }

    }
}
