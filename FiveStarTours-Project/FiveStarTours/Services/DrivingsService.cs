using FiveStarTours.Interfaces;
using FiveStarTours.Model;
using FiveStarTours.Repository;
using FiveStarTours.Serializer;
using FiveStarTours.View.Traveler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FiveStarTours.Services
{
    public class DrivingsService
    {
        private IDrivingsRepository _drivingRepository;

        public DrivingsService()
        {
            _drivingRepository = Injector.Injector.CreateInstance<IDrivingsRepository>();
        }
        public List<Drivings> GetAll()
        {
            return _drivingRepository.GetAll();
        }

        public List<string> GetAllNames()
        {
            return _drivingRepository.GetAllNames();

        }

        public Drivings Save(Drivings drivings)
        {
            return _drivingRepository.Save(drivings);   
        }

        public Drivings Delete(Drivings drivings)
        {
            return _drivingRepository.Delete(drivings);
        }

        public int NextId()
        {
            return NextId();
        }
    }
}
