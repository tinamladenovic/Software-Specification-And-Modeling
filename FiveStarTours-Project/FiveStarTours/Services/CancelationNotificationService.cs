using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveStarTours.Interfaces;
using FiveStarTours.Model;

namespace FiveStarTours.Services
{
    public class CancelationNotificationService
    {
        private ICancelationNotificationRepository _cancelationNotificationRepository;

        public CancelationNotificationService()
        {
            _cancelationNotificationRepository = Injector.Injector.CreateInstance<ICancelationNotificationRepository>();
        }

        public List<CancelationNotification> GetAll()
        {
            return _cancelationNotificationRepository.GetAll();
        }
        public CancelationNotification Update(CancelationNotification cancelation)
        {
            return _cancelationNotificationRepository.Update(cancelation);
        }
        public CancelationNotification Save(CancelationNotification cancelationNotification)
        {
            return _cancelationNotificationRepository.Save(cancelationNotification);
        }
        public int NextId()
        {
            return _cancelationNotificationRepository.NextId();
        }
    }
}
