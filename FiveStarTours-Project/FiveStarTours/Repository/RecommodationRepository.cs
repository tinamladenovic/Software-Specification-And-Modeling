using FiveStarTours.Interfaces;
using FiveStarTours.Model;
using FiveStarTours.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveStarTours.Repository
{
    public class RecommodationRepository: IRecommodationRepository
    {
        private const string FilePath = "../../../Resources/Data/recommodation.csv";

        private readonly Serializer<Recommodation> _serializer;

        private static RecommodationRepository instance = null;

        private List<Recommodation> _recommodations;
        public static RecommodationRepository GetInstace()
        {
            if (instance == null)
            {
                instance = new RecommodationRepository();
            }
            return instance;
        }
        public RecommodationRepository()
        {
            _serializer = new Serializer<Recommodation>();
            _recommodations = _serializer.FromCSV(FilePath);
        }

        public List<Recommodation> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Recommodation Save(Recommodation recommodation)
        {
           recommodation.Id = NextId();
            _recommodations = _serializer.FromCSV(FilePath);
            _recommodations.Add(recommodation);
            _serializer.ToCSV(FilePath, _recommodations);
            return recommodation;
        }

        public int NextId()
        {
            _recommodations = _serializer.FromCSV(FilePath);
            if (_recommodations.Count < 1)
            {
                return 1;
            }
             return _recommodations.Max(t => t.Id) + 1;
            
        }

        public Recommodation GetById(int id)
        {
            _recommodations = GetAll();
            foreach (Recommodation recommodation in _recommodations)
            {
                if (recommodation.Id == id)
                {
                    return recommodation;
                }
            }
            return null;
        }

        public Recommodation Update(Recommodation recommodation)
        {
            _recommodations = _serializer.FromCSV(FilePath);
            Recommodation current = _recommodations.Find(c => c.Id == recommodation.Id);
            int index = _recommodations.IndexOf(current);
            _recommodations.Remove(current);
            _recommodations.Insert(index, recommodation);
            _serializer.ToCSV(FilePath, _recommodations);
            return recommodation;
        }
    }
}
