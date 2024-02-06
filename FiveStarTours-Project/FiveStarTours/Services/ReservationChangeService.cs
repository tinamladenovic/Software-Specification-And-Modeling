using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveStarTours.Interfaces;
using FiveStarTours.Model;
using FiveStarTours.Repository;

namespace FiveStarTours.Services
{
    public class ReservationChangeService
    {
        private IReservationChangeRepository _reservationChangeRepository;
        public ReservationChangeService()
        {
            _reservationChangeRepository = Injector.Injector.CreateInstance <IReservationChangeRepository>();
        }
        public List<ReservationChange> GetAll()
        {
            return _reservationChangeRepository.GetAll();
        }

        public List<ReservationChange> GetAllProcessing()
        {
            return _reservationChangeRepository.GetAllProcessing();
        }

        public ReservationChange Save(ReservationChange changes)
        {
            return _reservationChangeRepository.Save(changes);
        }

        public void BindAccommodationReservation()
        {
            _reservationChangeRepository.BindAccommodationReservation();
        }

        public int NextId()
        {
            return _reservationChangeRepository.NextId();
        }

        public ReservationChange GetById(int id)
        {
            return _reservationChangeRepository.GetById(id);
        }

        public ReservationChange Update(ReservationChange changes)
        {
            return _reservationChangeRepository.Update(changes);
        }

        public bool IsBusy(ReservationChange changes)
        {
            return _reservationChangeRepository.IsBusy(changes);
        }
    }
}
