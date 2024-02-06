using FiveStarTours.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveStarTours.Model
{
    public class SuperGuestTitle : ISerializable
    {
        public int Id { get; set; }
        public User User { get; set; }
        public DateTime StartTime { get; set; }
        public int DurationYears { get; set; }
        public int Points { get; set; }
        public bool Valid { get; set; }

        public SuperGuestTitle()
        {

        }
        public SuperGuestTitle(User user, DateTime startTime, int durationYears, int points, bool valid)
        {

            User = user;
            StartTime = startTime;
            DurationYears = durationYears;
            this.Points = points;
            Valid = valid;
        }
        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), User.Id.ToString(), string.Join(';', StartTime), DurationYears.ToString(), Points.ToString(), Valid.ToString() };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            User = new User() { Id = int.Parse(values[1]) };
            StartTime = Convert.ToDateTime(values[2]);
            DurationYears = int.Parse(values[3]);
            Points = int.Parse(values[4]);
            Valid= Convert.ToBoolean(values[5]);
        }

        
    }
}
