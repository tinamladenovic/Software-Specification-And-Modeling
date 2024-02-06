using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveStarTours.Serializer;

namespace FiveStarTours.Model
{
    public class KeyPoints : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Visited { get; set; }

        public KeyPoints() { }

        public KeyPoints(string name)
        {
            Name = name;
        }

        public KeyPoints(string name, bool visited)
        {
            Name = name;
            Visited = visited;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
           {
                Id.ToString(),
                Name,
                Convert.ToString(Visited)
            };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Name = values[1];
            Visited = bool.Parse(values[2]);

        }
    }
}
