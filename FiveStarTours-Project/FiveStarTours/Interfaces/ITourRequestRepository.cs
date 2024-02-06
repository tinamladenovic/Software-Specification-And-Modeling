using FiveStarTours.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveStarTours.Interfaces
{
    public interface ITourRequestRepository
    {
        List<TourRequest> GetAll();
        TourRequest Save(TourRequest tourRequest);
        int NextId();
        TourRequest GetById(int id);
      
    }
}
