using FiveStarTours.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveStarTours.Interfaces
{
    public interface ILocationsRepository
    {
        List<Location> GetAll();
        List<string> GetAllStates();
        Location GetById(int id);
        List<string> GetCitiesInState(string state);
    }
}
