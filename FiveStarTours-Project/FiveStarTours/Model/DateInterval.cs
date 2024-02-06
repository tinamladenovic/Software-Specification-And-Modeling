using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveStarTours.Model
{
    public class DateInterval
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public DateInterval(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
                string.Join(';', Start),
                string.Join(';', End)
            };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            Start = Convert.ToDateTime(values[0]);
            End = Convert.ToDateTime(values[1]);
        }
    }
}
