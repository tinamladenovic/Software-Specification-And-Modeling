using FiveStarTours.Interfaces;
using FiveStarTours.Model;
using FiveStarTours.Repository;
using FiveStarTours.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveStarTours.Services
{
    public class ReservedDrivingsService
    {
        private IReservedDrivingsRepository _reservedDrivingsRepository;
        public ReservedDrivingsService()
        {
            _reservedDrivingsRepository = Injector.Injector.CreateInstance<IReservedDrivingsRepository>();
        }
        public List<ReservedDrivings> GetAll()
        {
            return _reservedDrivingsRepository.GetAll();
        }

        public ReservedDrivings Save(ReservedDrivings reserved)
        {
            return _reservedDrivingsRepository.Save(reserved);
        }

        public int NextId()
        {
            return _reservedDrivingsRepository.NextId();
        }
        public bool SearchStartingLocation(ReservedDrivings reserved, string state, string city)
        {
            return _reservedDrivingsRepository.SearchStartingLocation(reserved, state, city);
        }
        public bool SearchDestinationLocation(ReservedDrivings reserved, string state, string city)
        {
            return _reservedDrivingsRepository.SearchDestinationLocation(reserved, state, city);
        }
        public List<ReservedDrivings> GetAllVehicleBindLocation()
        {
            return _reservedDrivingsRepository.GetAllVehicleBindLocation();
        }

        public List<ReservedDrivings> SearchVehicles(string state, string city)
        {
            return _reservedDrivingsRepository.SearchVehicles(state, city);
        }
    }
 
}
