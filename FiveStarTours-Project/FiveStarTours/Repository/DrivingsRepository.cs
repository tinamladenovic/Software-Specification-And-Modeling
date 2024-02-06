using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveStarTours.Interfaces;
using FiveStarTours.Model;
using FiveStarTours.Serializer;
using FiveStarTours.View.VehicleOnAdress;

namespace FiveStarTours.Repository
{
    public class DrivingsRepository : IDrivingsRepository
    {
        private const string FilePath = "../../../Resources/Data/reservedDrivings.csv";

        private readonly Serializer<Drivings> _serializer;

        private List<Drivings> _drivings;

        public DrivingsRepository()
        {
            _serializer = new Serializer<Drivings>();
            _drivings = _serializer.FromCSV(FilePath);
        }

        public List<Drivings> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }
        
        public List<string> GetAllNames()
        {
            List<Drivings> drivings = GetAll();
            List<string> states = drivings.Select(l => l.Name).Distinct().ToList();
            return states;
        }
        
       

        public Drivings Save(Drivings drivings)
        {
            drivings.Id = NextId();
            _drivings = _serializer.FromCSV(FilePath);
            _drivings.Add(drivings);
            _serializer.ToCSV(FilePath, _drivings);
            return drivings;
        }

        public Drivings Delete(Drivings drivings) 
        {
            drivings.Id = NextId();
            _drivings = _serializer.FromCSV(FilePath);
            _drivings.Remove(drivings);
           
            return drivings;
        }

        public int NextId()
        {
            _drivings = _serializer.FromCSV(FilePath);
            if (_drivings.Count < 1)
            {
                return 1;
            }
            return _drivings.Max(l => l.Id) + 1;
        }

        internal List<string> GetAllDrivings()
        {
            throw new NotImplementedException();
        }

        internal void Delete(object drivings)
        {
            throw new NotImplementedException();
        }
    }
}
