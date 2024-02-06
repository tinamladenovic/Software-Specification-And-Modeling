using FiveStarTours.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveStarTours.Interfaces
{
    public interface ITaximeterRepository
    {
        List<Taximeter> GetAll();
        Taximeter Save(Taximeter newTaximeter);
        int NextId();
    }
}
