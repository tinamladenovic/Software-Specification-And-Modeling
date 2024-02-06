using FiveStarTours.Repository;
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
    public class Taximeter : ISerializable
    {
        public int Id { get; set; }
        public string Timer { get; set; }
        public double Price { get; set; }
        public Taximeter()
        {
        }

        public Taximeter(string timer, double price)
        {
            Timer = timer;
            Price = price;
        }   

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);    
            Timer = Convert.ToString(values[1]);    
            Price = Convert.ToDouble(values[2]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Timer.ToString(),
                Price.ToString()

            };
            return csvValues;

        }
    }
}
