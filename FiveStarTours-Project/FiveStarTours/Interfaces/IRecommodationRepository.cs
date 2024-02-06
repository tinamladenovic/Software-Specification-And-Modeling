using FiveStarTours.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveStarTours.Interfaces
{
    public interface IRecommodationRepository
    {
        List<Recommodation> GetAll();
        Recommodation Save(Recommodation recommodation);
        int NextId();
        Recommodation GetById(int id);
        Recommodation Update(Recommodation recommodation);
    }
}
