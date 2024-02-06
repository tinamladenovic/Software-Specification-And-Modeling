using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Media;
using FiveStarTours.Repository;
using FiveStarTours.Serializer;

public enum AccommodationType
{
    apartment,
    house,
    cottage
}

namespace FiveStarTours.Model
{
    public class Accommodation : ISerializable
    {
        public int Id { get; set; }
        public string AccommodationName { get; set; }
        public Location Location { get; set; }
        public AccommodationType Type { get; set; }
        public int MaxGuestNum { get; set; }
        public int MinReservationDays { get; set; }
        public int DaysPossibleToCancel { get; set; } = 1; 
        public bool RecentlyRenovated { get; set; }

        public List<string> ImageURLs { get; set; }
        public ImageSource FrontImage { get; set; }

        public Accommodation() { }

        public Accommodation(string accommodationName, Location location, AccommodationType type, int maxGuestNum, int minReservationDays, int daysPossibleToCancel, List<string> imageURLs)
        {
            AccommodationName = accommodationName;
            Location = location;
            Type = type;
            MaxGuestNum = maxGuestNum;
            MinReservationDays = minReservationDays;
            DaysPossibleToCancel = daysPossibleToCancel;
            ImageURLs = imageURLs;
            FrontImage = null;
            RecentlyRenovated = false;
        }

        public string[] ToCSV()
        {
            StringBuilder imageURLsList = new StringBuilder();

            foreach (string imageURL in ImageURLs)
            {
                imageURLsList.Append(imageURL);
                imageURLsList.Append(" ,");
            }

            imageURLsList.Remove(imageURLsList.Length - 1, 1);

            string[] csvValues = {
                Id.ToString(),
                AccommodationName,
                Location.Id.ToString(),
                Type.ToString(),
                MaxGuestNum.ToString(),
                MinReservationDays.ToString(),
                DaysPossibleToCancel.ToString(),
                string.Join(';', ImageURLs),
                RecentlyRenovated.ToString(),

        };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            AccommodationName = values[1];
            Location = new Location() { Id = Convert.ToInt32(values[2]) };
            Type = Enum.Parse<AccommodationType>(values[3]);
            MaxGuestNum = int.Parse(values[4]);
            MinReservationDays = int.Parse(values[5]);
            DaysPossibleToCancel = int.Parse(values[6]);

            ImageURLs = values[7].Split(';').ToList();
            RecentlyRenovated = Convert.ToBoolean(values[8]);
        }

    }
}
