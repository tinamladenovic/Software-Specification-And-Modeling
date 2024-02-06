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
    public class ReservedDrivingsRepository: IReservedDrivingsRepository
    {
        private const string FilePath = "../../../Resources/Data/reservedDrivings.csv";

        private readonly Serializer<ReservedDrivings> _serializer;

        private List<ReservedDrivings> _reservedDrivingsRepository;
        public ReservedDrivingsRepository()
        {
            _serializer = new Serializer<ReservedDrivings>();
            _reservedDrivingsRepository = _serializer.FromCSV(FilePath);
        }

        public List<ReservedDrivings> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }



        public ReservedDrivings Save(ReservedDrivings reserved)
        {
            reserved.Id = NextId();
            _reservedDrivingsRepository = _serializer.FromCSV(FilePath);
            _reservedDrivingsRepository.Add(reserved);
            _serializer.ToCSV(FilePath, _reservedDrivingsRepository);
            return reserved;
        }

        public int NextId()
        {
            _reservedDrivingsRepository = _serializer.FromCSV(FilePath);
            if (_reservedDrivingsRepository.Count < 1)
            {
                return 1;
            }
            return _reservedDrivingsRepository.Max(t => t.Id) + 1;
        }
        public bool SearchStartingLocation(ReservedDrivings reserved, string state, string city)
        {
            return reserved.StartingLocation.State.ToLower().Contains(state.ToLower()) && reserved.StartingLocation.City.ToLower().Contains(city.ToLower());
        }
        public bool SearchDestinationLocation(ReservedDrivings reserved, string state, string city)
        {
            return reserved.DestinationLocation.State.ToLower().Contains(state.ToLower()) && reserved.DestinationLocation.City.ToLower().Contains(city.ToLower());
        }
        public List<ReservedDrivings> GetAllVehicleBindLocation()
        {
            LocationsRepository locationsRepository = new LocationsRepository();
            _reservedDrivingsRepository = _serializer.FromCSV(FilePath);
            foreach (ReservedDrivings reserved in _reservedDrivingsRepository)
            {
                int locId = reserved.StartingLocation.Id;
                reserved.StartingLocation = locationsRepository.GetById(locId);
            }
            return _reservedDrivingsRepository;
        }


        public List<ReservedDrivings> SearchVehicles(string state, string city)
        {
            List<ReservedDrivings> searchedVehicles = new List<ReservedDrivings>();
            foreach (ReservedDrivings reserved in _reservedDrivingsRepository)
            {

                if (!SearchStartingLocation(reserved, state, city))
                {
                    continue;
                }
                if (!SearchDestinationLocation(reserved, state, city))
                {
                    continue;
                }


                searchedVehicles.Add(reserved);
            }
            return searchedVehicles;
        }
    }


}
