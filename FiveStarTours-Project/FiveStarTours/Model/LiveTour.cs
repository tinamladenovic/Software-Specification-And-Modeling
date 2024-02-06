using System;
using System.Collections.Generic;
using System.Linq;
using FiveStarTours.Serializer;

namespace FiveStarTours.Model
{
    public class LiveTour : ISerializable
    {
        public int Id { get; set; }
        public int IdTour { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public List<int> IdKeyPoints { get; set; }
        public List<KeyPoints> KeyPoints { get; set; }
        public List<bool> KeyPointsVisited { get; set; }
        public List<string> Visitors { get; set; }
        public bool Started { get; set; }
        public bool Ended { get; set; }

        public LiveTour() { }

        public LiveTour(int idTour, string name, DateTime date, List<int> idKeyPoints, List<KeyPoints> keyPoints, List<bool> keyPointsVisited, bool started, bool ended)
        {
            IdTour = idTour;
            Name = name;
            Date = date;
            IdKeyPoints = idKeyPoints;
            KeyPoints = keyPoints;
            KeyPointsVisited = keyPointsVisited;
            Started = started;
            Ended = ended;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
           {
                Id.ToString(),
                IdTour.ToString(),
                Name,
                Convert.ToString(Date),
                string.Join(';', IdKeyPoints),
                String.Join(';', KeyPointsVisited),
                Convert.ToString(Started),
                Convert.ToString(Ended)
            };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            IdTour = Convert.ToInt32(values[1]);
            Name = values[2];
            Date = Convert.ToDateTime(values[3]);
            IdKeyPoints = ConvertToInt(values[4]);
            KeyPointsVisited = ConvertToBool(values[5]);
            Started = Convert.ToBoolean(values[6]);
            Ended = Convert.ToBoolean(values[7]);
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

        public List<bool> ConvertToBool(string values)
        {
            List<string> bools = values.Split(';').ToList();
            List<bool> result = new List<bool>();

            foreach (string b in bools)
            {
                bool boolean = Convert.ToBoolean(b);
                result.Add(boolean);
            }

            return result;
        }
    }
}
