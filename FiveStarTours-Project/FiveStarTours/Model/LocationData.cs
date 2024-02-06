using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveStarTours.Model;

namespace FiveStarTours.Model
{
    public class LocationData
    {
        public string Address { get; set; }
        public int NumberOfRides { get; set; }

        public LocationData() { }

        public LocationData(string address, int numberOfRides)
        { 
            this.Address = address;
            this.Address += " " + numberOfRides;    
                
        }
    }
}
