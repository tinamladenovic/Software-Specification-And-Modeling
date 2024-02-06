using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FiveStarTours.Serializer;
using System.Threading.Tasks;

namespace FiveStarTours.Model
{
    public class CancelationNotification : ISerializable
    {
        public int Id { get; set; }
        public User Owner { get; set; }
        public User Guest { get; set; }
        public bool Delivered { get; set; }

        public CancelationNotification(int id, User owner, User guest, bool delivered)
        {
            Id = id;
            Owner = owner;
            Guest = guest;
            Delivered = delivered;
        }

        public CancelationNotification()
        { }
        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(),
                Owner.Id.ToString(),
                Guest.Id.ToString(),
                Delivered.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Owner = new User() { Id = Convert.ToInt32(values[1]) };
            Guest = new User() { Id = Convert.ToInt32(values[2]) };
            Delivered = Convert.ToBoolean(values[3]);

        }



    }
}
