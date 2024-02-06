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
    public class DrivingStatisticsData : ISerializable
    {
        public int Id { get; set; }
        public string DrivingYear { get; set; }
        public int DrivingNumber { get; set; }
        public string DrivingDuration { get; set; }
        public double DrivingPrice { get; set; }

        public DrivingStatisticsData() { }

        public DrivingStatisticsData(int id, string drivingYear, int drivingNumber, string drivingDuration, double drivingPrice)
        {
            Id = id;
            DrivingYear = drivingYear;  
            DrivingNumber = drivingNumber;
            DrivingDuration = drivingDuration;
            DrivingPrice = drivingPrice;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            DrivingYear = Convert.ToString(values[1]);
            DrivingNumber = Convert.ToInt32(values[2]); 
            DrivingDuration = Convert.ToString(values[3]);
            DrivingPrice = Convert.ToDouble(values[4]);     
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            { 
              Id.ToString(),
              DrivingYear,
              DrivingNumber.ToString(),
              DrivingDuration,
              DrivingPrice.ToString() 
            };
        
            return csvValues;
        }
    }
}
