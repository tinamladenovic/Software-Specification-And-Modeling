using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveStarTours.Interfaces;
using FiveStarTours.Model;

namespace FiveStarTours.Services
{
    public class TourRequestService
    {
        private ITourRequestRepository _tourRequestRepository;
        public TourRequestService()
        {
            _tourRequestRepository = Injector.Injector.CreateInstance<ITourRequestRepository>();
        }

        public List<TourRequest> GetAll()
        {
            return _tourRequestRepository.GetAll();
        }

        public TourRequest Save(TourRequest tourRequest)
        {
            return _tourRequestRepository.Save(tourRequest);
        }

        public int NextId()
        {
            return _tourRequestRepository.NextId();
        }

        public TourRequest GetById(int id)
        {
            return _tourRequestRepository.GetById(id);
        }
    }
}
