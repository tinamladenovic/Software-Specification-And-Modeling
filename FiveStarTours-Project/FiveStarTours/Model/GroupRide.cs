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
    public class GroupRide : ISerializable
    {
        public int Id { get; set; }
        public int PassengerNumber { get; set; }
        public int IdStartingLocation { get; set; }
        public Location StartingLocation { get; set; }
        public string StartingStreet { get; set; }
        public int IdDestinationLocation { get; set; }
        public Location DestinationLocation { get; set; }
        public string DestinationStreet { get; set; }
        public int IdLanguage { get; set; }
        public Language Language { get; set; }
        public DateTime DateTime { get; set; }
        public bool DriverIsLate { get; set; }
        public bool VisitorIsLate { get; set; }


        public GroupRide() { }

        public GroupRide(int passengerNumber, int idStartingLocation, Location startingLocation, string startingStreet, int idDestinationLocation, Location destinationLocation, string destinationStreet, int idLanguage, Language language, DateTime dateTime, bool driverIsLate, bool visitorIsLate)
        {
            PassengerNumber = passengerNumber;
            IdStartingLocation = idStartingLocation;
            StartingLocation = startingLocation;
            StartingStreet = startingStreet;
            IdDestinationLocation = idDestinationLocation;
            DestinationLocation = destinationLocation;
            DestinationStreet = destinationStreet;
            IdLanguage = idLanguage;
            Language = language;
            DateTime = dateTime;
            DriverIsLate = driverIsLate;
            VisitorIsLate = visitorIsLate;
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
              Id.ToString(),
              PassengerNumber.ToString(),
              IdStartingLocation.ToString(),
              StartingStreet,
              IdDestinationLocation.ToString(),
              DestinationStreet,
              IdLanguage.ToString(),
              DateTime.ToString(),
              DriverIsLate.ToString(),
              VisitorIsLate.ToString(),

              
             };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            PassengerNumber = Convert.ToInt32(values[1]);
            IdStartingLocation = Convert.ToInt32(values[2]);
            StartingStreet = values[3];
            IdDestinationLocation = Convert.ToInt32(values[4]);
            DestinationStreet = values[5];
            IdLanguage = Convert.ToInt32(values[6]);
            DateTime = Convert.ToDateTime(values[7]);
            DriverIsLate = Convert.ToBoolean(values[8]);
            VisitorIsLate= Convert.ToBoolean(values[9]);
            
        }
        
    }
}
