using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveStarTours.Interfaces;
using FiveStarTours.Model;

namespace FiveStarTours.Services
{
    public class VehicleOnAdressService
    {
        private IVehicleOnAdressRepository _vehicleOnAdressRepository;
        public VehicleOnAdressService()
        {
            _vehicleOnAdressRepository = Injector.Injector.CreateInstance<IVehicleOnAdressRepository>();
        }

        public List<OnAdress> GetAll()
        {
            return _vehicleOnAdressRepository.GetAll();
        }

        public OnAdress Save(OnAdress newVehicleOnAdress)
        {
            return _vehicleOnAdressRepository.Save(newVehicleOnAdress);
        }

        public int NextId()
        {
            return _vehicleOnAdressRepository.NextId();
        }
    }
}
