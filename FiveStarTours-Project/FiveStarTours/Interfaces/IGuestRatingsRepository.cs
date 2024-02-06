using FiveStarTours.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveStarTours.Interfaces
{
    public interface IGuestRatingsRepository
    {
        List<GuestRating> GetAll();
        GuestRating Save(GuestRating rating);
        int NextId();
        GuestRating GetById(int id);
        GuestRating Update(GuestRating rating);
    }
}
