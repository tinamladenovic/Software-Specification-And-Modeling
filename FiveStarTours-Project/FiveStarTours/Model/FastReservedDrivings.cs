using FiveStarTours.Serializer;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace FiveStarTours.Model
{
    public class FastReservedDrivings : ISerializable
    {

        public int Id { get; set; }
        public Location Location { get; set; }
        public DateTime Time { get; set; }

        public FastReservedDrivings() { }

        public FastReservedDrivings(int id, Location location, DateTime time)
        {
            Id = id;
            Location = location;
            Time = time;
        }


        public string[] ToCSV()
        {
            string[] csvValues =
            {
              Id.ToString(),
              Location.ToString(),
              Time.ToString(),
             };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            //Location = Convert.ToString(values[1]);
            Time = Convert.ToDateTime(values[2]);
        }

       
    }
}
