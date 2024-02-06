
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveStarTours.Serializer;
using FiveStarTours.Model;
using FiveStarTours.View;
using FiveStarTours.Interfaces;

namespace FiveStarTours.Repository
{
    public class VehicleOnAdressRepository : IVehicleOnAdressRepository
    {
        private const string FilePath = "../../../Resources/Data/vehicleOnAdress.csv";

        private readonly Serializer<OnAdress> _serializer;

        private List<OnAdress> _vehicleOnAdress;

        public VehicleOnAdressRepository()
        {
            _serializer = new Serializer<OnAdress>();
            _vehicleOnAdress = _serializer.FromCSV(FilePath);
        }

        public List<OnAdress> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public OnAdress Save(OnAdress newVehicleOnAdress)
        {
            newVehicleOnAdress.Id = NextId();
            _vehicleOnAdress = _serializer.FromCSV(FilePath);
            _vehicleOnAdress.Add(newVehicleOnAdress);
            _serializer.ToCSV(FilePath, _vehicleOnAdress);
            return newVehicleOnAdress;
        }

        
        public int NextId()
        {
            _vehicleOnAdress = _serializer.FromCSV(FilePath);
            if (_vehicleOnAdress.Count < 1)
            {
                return 1;
            }
            return _vehicleOnAdress.Max(t => t.Id) + 1;
        }

        
    }
}
