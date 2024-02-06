using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using FiveStarTours.Repository;
using FiveStarTours.Serializer;

namespace FiveStarTours.Model
{
    public class Tour : ISerializable
    {
        public int Id { get; set; }
        public User User { get; set; }
        public string Name { get; set; }
        public int IdLocation { get; set; }
        public Location Location { get; set; }
        public string Description { get; set; }
        public List<int> IdLanguages { get; set; }
        public List<Language> Languages { get; set; }
        public int MaxGuests { get; set; }
        public List<int> IdKeyPoints { get; set; }
        public List<KeyPoints> KeyPoints { get; set; }
        public List<DateTime> Beginning { get; set; }
        public DateTime OneBeginningTime { get; set; }
        public int Duration { get; set; }
        public List<string> ImageUrls { get; set; }

        public ImageSource FrontImage { get; set; }

        public Tour() { }

        public Tour(string name, User user, int idLocation, Location location, string description, List<int> idLanguage, List<Language> language, int maxGuests, List<int> idKeyPoints, List<KeyPoints> keyPoints, List<DateTime> beginning, int duration, List<string> imageUrls)
        {
            Name = name;
            User = user;
            IdLocation = idLocation;
            Location = location;
            Description = description;
            IdLanguages = idLanguage;
            Languages = language;
            MaxGuests = maxGuests;
            IdKeyPoints = idKeyPoints;
            KeyPoints = keyPoints;
            Beginning = beginning;
            Duration = duration;
            ImageUrls = imageUrls;
            FrontImage = null;
        }

        public Tour(string name, User user, int idLocation, Location location, string description, List<int> idLanguage, List<Language> language, int maxGuests, List<int> idKeyPoints, List<KeyPoints> keyPoints, DateTime oneBeginningTime, int duration, List<string> imageUrls)
        {
            Name = name;
            User = user;
            IdLocation = idLocation;
            Location = location;
            Description = description;
            IdLanguages = idLanguage;
            Languages = language;
            MaxGuests = maxGuests;
            IdKeyPoints = idKeyPoints;
            KeyPoints = keyPoints;
            OneBeginningTime = oneBeginningTime;
            Duration = duration;
            ImageUrls = imageUrls;
            FrontImage = null;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            { Id.ToString(),
              User.Id.ToString(),
              Name,
              IdLocation.ToString(),
              Description,
              string.Join(';', IdLanguages),
              MaxGuests.ToString(),
              string.Join(';', IdKeyPoints),
              string.Join(';', Beginning),
              Duration.ToString(),
              string.Join(';', ImageUrls) };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            User = new User() { Id = Convert.ToInt32(values[1]) };
            Name = values[2];
            IdLocation = Convert.ToInt32(values[3]);
            Description = values[4];
            IdLanguages = ConvertToInt(values[5]);
            MaxGuests = Convert.ToInt32(values[6]);
            IdKeyPoints = ConvertToInt(values[7]);
            Beginning = ConvertToDateTime(values[8]);
            Duration = Convert.ToInt32(values[9]);
            ImageUrls = values[10].Split(';').ToList();

        }

        // Conversion from string to DateTime - for list
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

        public Location getLocationById(int locationId)
        {
            LocationsRepository locationsRepository = new LocationsRepository();
            foreach (Location location in locationsRepository.GetAll())
            {
                if (locationId == location.Id)
                {
                    Location = location;
                    return location;
                }
            }

            return null;

        }

        public List<KeyPoints> getKeyPointsById(List<int> keyPointsId)
        {
            KeyPointsRepository keyPointsRepository = new KeyPointsRepository();
            List<KeyPoints> result = new List<KeyPoints>();

            foreach (KeyPoints keyPoint in keyPointsRepository.GetAll())
            {
                foreach (int keypoint in keyPointsId)
                {
                    if (keyPoint.Id == keypoint)
                    {
                        result.Add(keyPoint);
                    }
                }
            }

            return result;
        }
    }
}