using FiveStarTours.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveStarTours.Interfaces
{
    public interface ICancelationNotificationRepository
    {
        List<CancelationNotification> GetAll();
        CancelationNotification Update(CancelationNotification cancelation);
        CancelationNotification Save(CancelationNotification cancelationNotification);
        int NextId();
    }
}
