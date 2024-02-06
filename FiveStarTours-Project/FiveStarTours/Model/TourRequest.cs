using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using FiveStarTours.Model.Enums;
using FiveStarTours.Repository;
using FiveStarTours.Serializer;
namespace FiveStarTours.Model
{
    public class TourRequest: ISerializable
    {
        public int Id { get; set; }
        public int IdLocation { get; set; }
        public Location Location { get; set; }
        public string Description { get; set; }
        public int IdLanguage { get; set; }
        public Language Language { get; set; }
        public int MaxGuests { get; set; }
        public DateInterval Intervals { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime AcceptDateTime { get; set; }
        public ReservationChangeStatusType Status { get; set; } = ReservationChangeStatusType.Processing;


        public TourRequest() { }

        public TourRequest(int idLocation, Location location, string description, int idLanguage, Language language, int maxGuests, 
              DateInterval intervals, DateTime dateTime) 
        {  
            IdLocation = idLocation;
            Location = location;
            Description = description;
            IdLanguage = idLanguage;
            Language = language;
            MaxGuests = maxGuests;
            Intervals = intervals;
            DateTime = dateTime;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            { 
              Id.ToString(),
              IdLocation.ToString(),
              Description,
              IdLanguage.ToString(),
              MaxGuests.ToString(),
              MakeInterval(Intervals),
              DateTime.ToString(),
              AcceptDateTime.ToString(),
              Status.ToString()};
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            IdLocation = Convert.ToInt32(values[1]);
            Location = FindLocation(IdLocation);
            Description = values[2];
            IdLanguage = Convert.ToInt32(values[3]);
            Language = FindLanguage(IdLanguage);
            MaxGuests = Convert.ToInt32(values[4]);
            Intervals = GetDateInterval(values[5]);
            DateTime = Convert.ToDateTime(values[6]);
            AcceptDateTime = Convert.ToDateTime(values[7]);
            Status = Enum.Parse<ReservationChangeStatusType>(values[8]);
        }

        private Location FindLocation(int id)
        {
            LocationsRepository locationsRepository = new LocationsRepository();
            Location location = null;
            foreach(Location l in locationsRepository.GetAll())
            {
                if(l.Id == id)
                {
                    location = l;
                }
            }
            return location;
        }

        private Language FindLanguage(int id)
        {
            LanguagesRepository languagesRepository = new LanguagesRepository();
            Language language = null;
            foreach(Language l in languagesRepository.GetAll())
            {
                if(l.Id == id)
                {
                    language = l;
                }
            }

            return language;
        }

        public List<DateTime> ConvertToDateTime(string values)
        {
            List<string> dates = values.Split(';').ToList();
            List<DateTime> result = new List<DateTime>();

            foreach (string date in dates)
            {
                DateTime beginning = Convert.ToDateTime(date);
                result.Add(beginning);
            }

            return result;
        }

        // Conversion from string to int - for list
        public List<int> ConvertToInt(string values)
        {
            List<string> numbers = values.Split(';').ToList();
            List<int> result = new List<int>();

            foreach (string num in numbers)
            {
                int number = Convert.ToInt32(num);
                result.Add(number);
            }

            return result;

        }

        public string MakeInterval(DateInterval Interval)
        {
            string result = Interval.Start.ToString() + " - " + Interval.End.ToString();
            return result;
        }

        public DateInterval GetDateInterval(string values)
        {
            List<string> interval = values.Split(" - ").ToList();
            DateTime Start = DateTime.Parse(interval[0]);
            DateTime End = DateTime.Parse(interval[1]);
            return new DateInterval(Start, End);
        }
    }
}
