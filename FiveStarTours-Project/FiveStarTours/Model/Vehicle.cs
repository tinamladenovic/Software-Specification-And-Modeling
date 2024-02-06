using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using FiveStarTours.Repository;
using FiveStarTours.Serializer;


namespace FiveStarTours.Model
{
    public class Vehicle : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IdLocation { get; set; }
        public Location Location { get; set; }
        public int MaxPersonNum { get; set; }
        public List<int> IdLanguages { get; set; }
        public List<Language> Languages { get; set; }
        public List<string> ImageUrls { get; set; }

        public string FastDriveNum { get; set; }

        public Vehicle() { }

        public Vehicle( string name, int idlocation, Location location, int maximumPersonNumber,  List<Language> languageList, List<int> idLanguages, List<string> imageUrlsList, string fastdrivenum)
        {
           
            Name = name;
            IdLocation = idlocation;
            Location = location;
            MaxPersonNum = maximumPersonNumber;
            IdLanguages = idLanguages;
            Languages = languageList;
            ImageUrls = imageUrlsList;
            FastDriveNum = fastdrivenum;
        }

        public string[] ToCSV()
        {
            
            string[] csvValues =
            { 
              Id.ToString(),
              Name,
              IdLocation.ToString(),
              string.Join(';', IdLanguages),
              MaxPersonNum.ToString(),
              string.Join(';', ImageUrls),
              FastDriveNum
            };
            return csvValues;
        }
            
        

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            IdLocation = Convert.ToInt32(values[2]);
            IdLanguages = ConvertToInt(values[3]);
            MaxPersonNum = Convert.ToInt32(values[4]);
            ImageUrls = values[5].Split(';').ToList();
            FastDriveNum = values[6];
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
    }
}
