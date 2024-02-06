using FiveStarTours.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveStarTours.Model
{
    public class Drivings : ISerializable
    {

        public int Id { get; set; }
        public string Name { get; set; }

        public Drivings() { }

        public Drivings(string name)
        {
            Name = name;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Name
            };
            return csvValues;
        }
        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);

            Name = Convert.ToString(values[1]);
        }

        
    }
}
