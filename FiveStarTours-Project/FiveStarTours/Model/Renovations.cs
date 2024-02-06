using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Xml.Linq;
using FiveStarTours.Repository;
using FiveStarTours.Serializer;


namespace FiveStarTours.Model
{
    public class Renovations : ISerializable
    {
        public int Id { get; set; }
        public User User { get; set; }
        public string AccommodationName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ExpectedDuration { get; set; }
        public string Description { get; set; }


        public Renovations() { }

        public Renovations(string accommodationName, DateTime start, DateTime end, int expected, string description)
        {
            AccommodationName = accommodationName;
            StartDate= start;
            EndDate= end;
            ExpectedDuration = expected;
            Description = description;
        }

        public string[] ToCSV()
        {
            string[] csvValues = {
              Id.ToString(),
              AccommodationName,
              string.Join(';', StartDate),
              string.Join(';', EndDate),
              ExpectedDuration.ToString(),
              Description

        };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            AccommodationName = values[1];
            StartDate = Convert.ToDateTime(values[2]);
            EndDate = Convert.ToDateTime(values[3]);
            ExpectedDuration = Convert.ToInt32(values[4]);
            Description = values[5];
        }






    }
}
