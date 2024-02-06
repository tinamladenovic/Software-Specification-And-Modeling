using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveStarTours.Model
{
    public static class Notification
    {
        public static bool SentNotification { get; set; }
        public static bool Answer { get; set; }
        public static User? User { get; set; }

    }
}
