using System;
using System.Collections.Generic;
using System.Linq;
using FiveStarTours.Serializer;
using System.Text;
using System.Threading.Tasks;

namespace FiveStarTours.Model
{
    public class Recommodation : ISerializable
    {
        public int Id { get; set; }
        public AccommodationReservation AccommodationReservation { get; set; }
       public string RecommodationText { get; set; }
        public string Necessity { get; set; }

        public Recommodation()
        {
            AccommodationReservation = new AccommodationReservation();
        }
        public Recommodation(AccommodationReservation accommodationReservation, string recommodationText, string necessity)
        {
           
            AccommodationReservation = accommodationReservation;
            RecommodationText = recommodationText;
            Necessity = necessity;
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                AccommodationReservation.AccommodationName,
                RecommodationText,
                Necessity
            };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            AccommodationReservation = new AccommodationReservation() { AccommodationName = values[1] };
            RecommodationText = values[2];
            Necessity = values[3];
        }
    }
}

