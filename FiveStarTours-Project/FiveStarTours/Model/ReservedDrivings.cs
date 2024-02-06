using FiveStarTours.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace FiveStarTours.Model
{
    public class ReservedDrivings: ISerializable
    {
        public int Id { get; set; }
        public int IdStartingLocation { get; set; }
        public int IdDestinationLocation { get; set; }
        public Location StartingLocation { get; set; }
        public string StartingStreet { get; set; }
        public Location DestinationLocation { get; set; }
        public string DestinationStreet { get; set; }
        public DateTime StartingTime { get; set; }
        public int UserId { get; set; }


        public ReservedDrivings() { }
        public ReservedDrivings(int idStartingLocation,int idDestinationLocation, Location startingLocation, string startingStreet, Location destinationLocation, string destinationStreet, DateTime startingTime, int userId)
        {
           IdStartingLocation = idStartingLocation;
            IdDestinationLocation = idDestinationLocation;
            StartingLocation = startingLocation;
            StartingStreet = startingStreet;
            DestinationLocation = destinationLocation;
            DestinationStreet = destinationStreet;
            StartingTime = startingTime;
            UserId = userId;
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
              Id.ToString(),
              IdStartingLocation.ToString(),
              StartingStreet,
              IdDestinationLocation.ToString(),
              DestinationStreet,
              StartingTime.ToString(),
              UserId.ToString()
             };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            IdStartingLocation = Convert.ToInt32(values[1]);
            StartingStreet = values[2];
            IdDestinationLocation = Convert.ToInt32(values[3]);
            DestinationStreet = values[4];
            StartingTime = Convert.ToDateTime(values[5]);
            UserId = Convert.ToInt32(values[6]);
        }
    }
}
