using FiveStarTours.Interfaces;
using FiveStarTours.Model;
using FiveStarTours.Serializer;
using FiveStarTours.View.VehicleOnAdress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveStarTours.Repository
{
    class TaximeterRepository : ITaximeterRepository
    {
        private const string FilePath = "../../../Resources/Data/taximeter.csv";
        private readonly Serializer<Taximeter> _serializer;

        private List<Taximeter> _taximeter;

        public TaximeterRepository()
        {
            _serializer = new Serializer<Taximeter>();
            _taximeter = _serializer.FromCSV(FilePath);
        }
        public List<Taximeter> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Taximeter Save(Taximeter newTaximeter)
        {
            newTaximeter.Id = NextId();
            _taximeter = _serializer.FromCSV(FilePath);
            _taximeter.Add(newTaximeter);
            _serializer.ToCSV(FilePath, _taximeter);
            return newTaximeter;
        }

        public int NextId()
        {
            _taximeter = _serializer.FromCSV(FilePath);
            if (_taximeter.Count < 1)
            {
                return 1;
            }
            return _taximeter.Max(t => t.Id) + 1;
        }
    }
}
