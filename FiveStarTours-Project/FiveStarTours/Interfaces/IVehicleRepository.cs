using FiveStarTours.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveStarTours.Interfaces
{
    public interface IVehicleRepository
    {
        List<Vehicle> GetAll();
        Vehicle Save(Vehicle vehicle);
        int NextId();
    }
}
