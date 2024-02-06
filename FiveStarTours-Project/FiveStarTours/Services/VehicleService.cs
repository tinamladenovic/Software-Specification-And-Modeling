using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveStarTours.Interfaces;
using FiveStarTours.Model;

namespace FiveStarTours.Services
{
    public class VehicleService
    {
        private IVehicleRepository _vehicleRepository;
        public VehicleService()
        {
            _vehicleRepository = Injector.Injector.CreateInstance<IVehicleRepository>();
        }
        public List<Vehicle> GetAll()
        {
            return _vehicleRepository.GetAll(); 
        }

        public Vehicle Save(Vehicle vehicle)
        {
            return _vehicleRepository.Save(vehicle);
        }

        public int NextId()
        {
            return _vehicleRepository.NextId();
        }
    }
}
