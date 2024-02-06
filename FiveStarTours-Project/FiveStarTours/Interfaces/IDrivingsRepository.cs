using FiveStarTours.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveStarTours.Interfaces
{
    public interface IDrivingsRepository
    {
        List<Drivings> GetAll();
        List<string> GetAllNames();
        Drivings Save(Drivings drivings);
        Drivings Delete(Drivings drivings);
        int NextId();

    }
}
