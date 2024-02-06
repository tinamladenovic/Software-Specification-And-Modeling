using FiveStarTours.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FiveStarTours.Model
{
    public class GiftCard : ISerializable
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime ExpiringDate { get; set; }

        public GiftCard() { }

        public GiftCard( int userId, DateTime expiringDate)
        {
            UserId = userId;
            ExpiringDate = expiringDate;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), UserId.ToString(), ExpiringDate.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            UserId = Convert.ToInt32(values[1]);
            ExpiringDate = Convert.ToDateTime(values[2]);

        }


    }
}
