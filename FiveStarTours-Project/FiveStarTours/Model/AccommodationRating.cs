using System;
using System.Collections.Generic;
using System.Linq;
using FiveStarTours.Serializer;

using System.Text;
using System.Threading.Tasks;

namespace FiveStarTours.Model
{
    public class AccommodationRating : ISerializable
    {
        public int Id { get; set; }
        public AccommodationReservation AccommodationReservation { get; set; }
        public int RaitingOwner { get; set; }
        public int AccCleanness { get; set; }
        public int AccAsInPicture { get; set; }
        public int AccCorectness { get; set; }
        public int AccExperience { get; set; }
        public string Comment { get; set; }
        public List<string> ImageURLs { get; set; }



        public AccommodationRating()
        {
            AccommodationReservation = new AccommodationReservation();
        }
        public AccommodationRating(AccommodationReservation accommodationReservation, int raitingOwner, int accCleanness, int accAsInPicture, int accCorectness, int accExperience, string comment,  List<string> imageURLs)
        {
            
            AccommodationReservation = accommodationReservation;
            RaitingOwner = raitingOwner;
            AccCleanness = accCleanness;
            AccAsInPicture = accAsInPicture;
            AccCorectness = accCorectness;
            AccExperience = accExperience;
            Comment = comment;
            ImageURLs = imageURLs;
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
            string[] csvValues =
            {
                Id.ToString(),
                AccommodationReservation.AccommodationName,
                RaitingOwner.ToString(),
                AccCleanness.ToString(),
                AccAsInPicture.ToString(),
                AccCorectness.ToString(),
                AccExperience.ToString(),
                Comment,
                string.Join(';', ImageURLs)
            };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            AccommodationReservation = new AccommodationReservation() { AccommodationName = values[1] };
            RaitingOwner = int.Parse(values[2]);
            AccCleanness = int.Parse(values[3]);
            AccAsInPicture= int.Parse(values[4]);
            AccCorectness = int.Parse(values[5]);
            AccExperience = int.Parse(values[6]);
            Comment = values[7];
            ImageURLs = values[8].Split(';').ToList();
        }
    }

}
