using FiveStarTours.Model;
using FiveStarTours.Repository;
using FiveStarTours.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveStarTours.Interfaces
{
    public interface IReservedDrivingsRepository
    {
        public List<ReservedDrivings> GetAll();
        public ReservedDrivings Save(ReservedDrivings reserved);
        public int NextId();
        public bool SearchStartingLocation(ReservedDrivings reserved, string state, string city);
        public bool SearchDestinationLocation(ReservedDrivings reserved, string state, string city);
        public List<ReservedDrivings> GetAllVehicleBindLocation();
        public List<ReservedDrivings> SearchVehicles(string state, string city);
        
    }
}
