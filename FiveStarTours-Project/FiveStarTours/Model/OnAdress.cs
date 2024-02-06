using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Documents;
using FiveStarTours.Repository;
using FiveStarTours.Serializer;
using FiveStarTours.Model;
using FiveStarTours.View;

namespace FiveStarTours.Model
{
    public class OnAdress : ISerializable
    {

        public int Id { get; set; }
        
        public Drivings Name { get; set; }
        public bool FastDrive { get; set; }
        public bool IsOnAdress { get; set; }
        public bool IsDelay { get; set; }
        public int Delays { get; set; }
        public bool IsDrivingStarted { get; set; }
        public int StartPrice { get; set; }
        



        public OnAdress() { }

        public OnAdress( Drivings name, bool fastDrive, bool isOnAdress, bool isDelay, int delays, bool isDrivingStarted, int startPrice)
        {

            Name = name;
            
            FastDrive = fastDrive;
            IsOnAdress = isOnAdress;
            IsDelay = isDelay;
            Delays = delays;
            IsDrivingStarted = isDrivingStarted;
            StartPrice = startPrice;
            
        }

        public string[] ToCSV()
        {

            string[] csvValues =
            {
                Id.ToString(),
                //Name.ToString(),
                
                FastDrive.ToString(),
                IsOnAdress.ToString(),
                IsDelay.ToString(),
                Delays.ToString(),
                IsDrivingStarted.ToString(),
                StartPrice.ToString(),
                  
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            
            FastDrive = Convert.ToBoolean(values[1]);
            IsOnAdress = Convert.ToBoolean(values[2]);
            IsDelay = Convert.ToBoolean(values[3]);
            Delays = Convert.ToInt32(values[4]);
            IsDrivingStarted.GetType().ToString();
            StartPrice.GetType().ToString();

        }


        public bool GetIsOnAdress()
        {
            return IsOnAdress;
        }

        public bool GetIsDelay()
        {
            return IsDelay;
        }
        public int GetDelays() 
        {
            return Delays;
        }

        
    }
}
