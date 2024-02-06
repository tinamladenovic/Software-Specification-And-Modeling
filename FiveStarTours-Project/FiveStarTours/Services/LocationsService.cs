using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveStarTours.Interfaces;
using FiveStarTours.Model;

namespace FiveStarTours.Services
{
    public class LocationsService
    {
        private ILocationsRepository _locationsRepository;
        public LocationsService()
        {
            _locationsRepository = Injector.Injector.CreateInstance<ILocationsRepository>();
        }

        public List<Location> GetAll()
        {
            return _locationsRepository.GetAll();
        }

        public List<string> GetAllStates()
        {
            return _locationsRepository.GetAllStates();
        }

        public Location GetById(int id)
        {
           return _locationsRepository.GetById(id);
        }
        public List<string> GetCitiesInState(string state)
        {
            return _locationsRepository.GetCitiesInState(state);
        }
    }
}
