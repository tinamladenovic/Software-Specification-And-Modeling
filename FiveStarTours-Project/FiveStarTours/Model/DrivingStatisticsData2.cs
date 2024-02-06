using FiveStarTours.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Xml.Linq;

namespace FiveStarTours.Model
{
    public class DrivingStatisticsData2 : ISerializable
    {
        public int Id { get; set; }
        public string DrivingYear2 { get; set; }
        public string DrivingNDP { get; set; }
        public string January { get; set; }
        public string February { get; set; }
        public string March { get; set; }
        public string April { get; set; }
        public string May { get; set; }
        public string June { get; set; }
        public string July { get; set; }
        public string August { get; set; }
        public string September { get; set; }
        public string October { get; set; }
        public string November { get; set; }
        public string December { get; set; }



        public DrivingStatisticsData2() { }

        public DrivingStatisticsData2 (int id, string drivingYear2, string drivingNDP, string january, string february, string march, string april, 
            string may, string june, string july, string august, string september, string october, string november, string december)
        {
            Id = id;
            DrivingYear2 = drivingYear2;
            DrivingNDP = drivingNDP;
            January = january;
            February = february;
            March = march;
            April = april;
            May = may;
            June = june;
            July = july;
            August = august;
            September = september;
            October = october;
            November = november;
            December = december;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            DrivingYear2 = Convert.ToString(values[1]);
            DrivingNDP = Convert.ToString(values[2]);   
            January = Convert.ToString(values[3]);
            February = Convert.ToString(values[4]);
            March = Convert.ToString(values[5]);
            April = Convert.ToString(values[6]);
            May = Convert.ToString(values[7]);
            June = Convert.ToString(values[8]);
            July = Convert.ToString(values[9]);
            August = Convert.ToString(values[10]);
            September = Convert.ToString(values[11]);
            October = Convert.ToString(values[12]);
            November = Convert.ToString(values[13]);
            December = Convert.ToString(values[14]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
           {
              Id.ToString(),
              DrivingYear2, 
              DrivingNDP,
              January,
              February,
              March,
              April,
              May,
              June,
              July,
              August,
              September,
              October,
              November,
              December,
            };

            return csvValues;
        }

        
    }
}
