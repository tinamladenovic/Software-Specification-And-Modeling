// Modeled on CommentRepository from InitialProject

using System.Collections.Generic;
using System.Linq;
using FiveStarTours.Interfaces;
using FiveStarTours.Model;
using FiveStarTours.Serializer;

namespace FiveStarTours.Repository
{
    public class LocationsRepository : ILocationsRepository
    {
        private const string FilePath = "../../../Resources/Data/locations.csv";

        private readonly Serializer<Location> _serializer;

        private List<Location> _locations;

        public LocationsRepository()
        {
            _serializer = new Serializer<Location>();
            _locations = _serializer.FromCSV(FilePath);
        }

        public List<Location> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public List<string> GetAllStates()
        {
            List<Location> locations = GetAll();
            List<string> states = locations.Select(l => l.State).Distinct().ToList();
            return states;
        }

        public Location GetById(int id)
        {
            foreach(Location location in _locations)
            {
                if(location.Id == id)
                {
                    return location;
                }
            }
            return null;
        }
        public List<string> GetCitiesInState(string state)
        {
            List<string> result = new List<string>();
            var locations = GetAll();
            foreach(Location location in locations)
            {
                if(location.State == state)
                {
                    result.Add(location.City);
                }
            }
            return result;
        }
    }
}
