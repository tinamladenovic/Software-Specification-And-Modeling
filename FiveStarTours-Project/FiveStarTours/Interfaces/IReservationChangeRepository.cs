using FiveStarTours.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveStarTours.Interfaces
{
    public interface IReservationChangeRepository
    {
        List<ReservationChange> GetAll();
        List<ReservationChange> GetAllProcessing();
        ReservationChange Save(ReservationChange changes);
        void BindAccommodationReservation();
        int NextId();
        ReservationChange GetById(int id);
        ReservationChange Update(ReservationChange changes);

        bool IsBusy(ReservationChange changes);


    }
}
