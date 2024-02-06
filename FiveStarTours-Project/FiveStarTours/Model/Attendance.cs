using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveStarTours.Serializer;

namespace FiveStarTours.Model
{
    public class Attendance : ISerializable
    {
        public int Id { get; set; }
        public int IdLiveTour { get; set; }
        public DateTime Date { get; set; }
        public int IdTour { get; set; }
        public int IdVisitor { get; set; }
        public int IdKeyPoint { get; set; }

        public Attendance() { }
        public Attendance(int idLiveTour, int idTour, DateTime date, int idVisitor, int isKeyPoint)
        {
            IdLiveTour = idLiveTour; 
            IdTour = idTour;
            Date = date;
            IdVisitor = idVisitor;
            IdKeyPoint = isKeyPoint;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
          {
              Id.ToString(),
              IdLiveTour.ToString(),
              Date.ToString(),
              IdTour.ToString(),
              IdVisitor.ToString(),
              IdKeyPoint.ToString(),
            };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            IdLiveTour = Convert.ToInt32(values[1]);
            Date = Convert.ToDateTime(values[2]);
            IdTour = Convert.ToInt32(values[3]);
            IdVisitor = Convert.ToInt32(values[4]);
            IdKeyPoint = Convert.ToInt32(values[5]);
            
        }
    }
}
