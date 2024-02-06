using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveStarTours.Model
{
    public class NotificationManager
    {
        private static NotificationManager instance;
        public static NotificationManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new NotificationManager();
                return instance;
            }
        }

        public string Notification { get; set; }
    }
}
