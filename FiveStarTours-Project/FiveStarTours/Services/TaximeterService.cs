using FiveStarTours.Interfaces;
using FiveStarTours.Model;
using FiveStarTours.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveStarTours.Services
{
    public class TaximeterService
    {
        private ITaximeterRepository _taximeterRepository;

        public TaximeterService()
        {
            _taximeterRepository = Injector.Injector.CreateInstance<ITaximeterRepository>();
        }

        public List<Taximeter> GetAll()
        {
            return _taximeterRepository.GetAll();
        }

        public Taximeter Save(Taximeter newTaximeter)
        {
            return _taximeterRepository.Save(newTaximeter);
        }

        public int NextId()
        {
            return NextId();
        }
    }
}
