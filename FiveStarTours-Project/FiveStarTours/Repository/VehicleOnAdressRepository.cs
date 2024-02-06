
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveStarTours.Serializer;
using FiveStarTours.Model;
using FiveStarTours.View;
using FiveStarTours.Interfaces;
using FiveStarTours.View.VehicleOnAdress;

namespace FiveStarTours.Repository
{
    public class VehicleOnAdressRepository : IVehicleOnAdressRepository
    {
        private const string FilePath = "../../../Resources/Data/vehicleOnAdress.csv";

        private readonly Serializer<OnAdress> _serializer;
        private static VehicleOnAdressRepository instance = null;

        private List<OnAdress> _vehicleOnAdress;

        public static VehicleOnAdressRepository GetInstace()
        {
            if (instance == null)
            {
                instance = new VehicleOnAdressRepository();
            }
            return instance;
        }
        public VehicleOnAdressRepository()
        {
            _serializer = new Serializer<OnAdress>();
            _vehicleOnAdress = _serializer.FromCSV(FilePath);
        }

        public List<OnAdress> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public OnAdress Save(OnAdress newVehicleOnAdress)
        {
            newVehicleOnAdress.Id = NextId();
            _vehicleOnAdress = _serializer.FromCSV(FilePath);
            _vehicleOnAdress.Add(newVehicleOnAdress);
            _serializer.ToCSV(FilePath, _vehicleOnAdress);
            return newVehicleOnAdress;
        }

        
        public int NextId()
        {
            _vehicleOnAdress = _serializer.FromCSV(FilePath);
            if (_vehicleOnAdress.Count < 1)
            {
                return 1;
            }
            return _vehicleOnAdress.Max(t => t.Id) + 1;
        }


        // get all fast drive 
        public List<OnAdress> GetAllFastDrive()
        {
            List<OnAdress> fastDrives = new List<OnAdress>();
            OnAdress fastDrive1 = new OnAdress();
            fastDrives.Add(fastDrive1);
            OnAdress fastDrive2 = new OnAdress();
            fastDrives.Add(fastDrive2);
            OnAdress fastDrive3 = new OnAdress();
            fastDrives.Add(fastDrive3);
            OnAdress fastDrive4 = new OnAdress();
            fastDrives.Add(fastDrive4);
            OnAdress fastDrive5 = new OnAdress();
            fastDrives.Add(fastDrive5);
            OnAdress fastDrive6 = new OnAdress();
            fastDrives.Add(fastDrive6);
            OnAdress fastDrive7 = new OnAdress();
            fastDrives.Add(fastDrive7);
            OnAdress fastDrive8 = new OnAdress();
            fastDrives.Add(fastDrive8);
            OnAdress fastDrive9 = new OnAdress();
            fastDrives.Add(fastDrive9);
            OnAdress fastDrive10 = new OnAdress();
            fastDrives.Add(fastDrive10);
            OnAdress fastDrive11 = new OnAdress();
            fastDrives.Add(fastDrive11);
            OnAdress fastDrive12 = new OnAdress();
            fastDrives.Add(fastDrive12);
            OnAdress fastDrive13 = new OnAdress();
            fastDrives.Add(fastDrive13);
            OnAdress fastDrive14 = new OnAdress();
            fastDrives.Add(fastDrive14);
            OnAdress fastDrive15 = new OnAdress();
            fastDrives.Add(fastDrive15);
            OnAdress fastDrive16 = new OnAdress();
            fastDrives.Add(fastDrive16);
            return fastDrives;
        }
        //fast drive count
        public int FastDriveCount()
        {
            List<OnAdress> fastDrives = GetAllFastDrive();
            int count = fastDrives.Count;
            return count;
        }


    }
}
