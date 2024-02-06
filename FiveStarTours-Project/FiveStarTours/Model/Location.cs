using FiveStarTours.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace FiveStarTours.Model
{
    public class Location : FiveStarTours.Serializer.ISerializable
    {
        public string selectedState;
        public string selectedCity;

        public int Id { get; set; }
        public string State { get; set; }
        public string City { get; set; }

        public Location() { }
        
        public Location(string selectedState, string selectedCity)
        {
            
            this.selectedState = selectedState;
            this.selectedCity = selectedCity;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                selectedState,
                selectedCity
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            State = values[1];
            City = values[2];
        }
    }
}

