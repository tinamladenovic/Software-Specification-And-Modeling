using FiveStarTours.Interfaces;
using FiveStarTours.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveStarTours.Services
{
    public class RecommodationService
    {
        private IRecommodationRepository _recommodationRepository;

        public RecommodationService()
        {
            _recommodationRepository = Injector.Injector.CreateInstance<IRecommodationRepository>();
        }

        public List<Recommodation> GetAll()
        {
            return _recommodationRepository.GetAll();
        }

        public Recommodation Save(Recommodation recommodation)
        {
            return _recommodationRepository.Save(recommodation);
        }

        public int NextId()
        {
            return NextId();
        }
        public Recommodation GetById(int id)
        {
            return _recommodationRepository.GetById(id);
        }

        public Recommodation Update(Recommodation recommodation)
        {
            return _recommodationRepository.Update(recommodation);
        }
    }
}
