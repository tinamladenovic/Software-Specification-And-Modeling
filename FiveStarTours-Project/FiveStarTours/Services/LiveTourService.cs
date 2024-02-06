using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveStarTours.Interfaces;
using FiveStarTours.Model;

namespace FiveStarTours.Services
{
    public class LiveTourService
    {
        private ILiveTourRepository _liveTourRepository;

        public LiveTourService()
        {
            _liveTourRepository = Injector.Injector.CreateInstance <ILiveTourRepository>();
        }

        public List<LiveTour> GetAll()
        {
            return _liveTourRepository.GetAll();    
        }

        public LiveTour Save(LiveTour liveTour)
        {
            return _liveTourRepository.Save(liveTour);
        }

        public int NextId()
        {
            return _liveTourRepository.NextId();
        }

        public void FindIdAndSave(LiveTour liveTour, int idLiveTour)
        {
            _liveTourRepository.FindIdAndSave(liveTour, idLiveTour);    
        }

        public List<string> GetEndedTours(List<Tour> tours)
        {
            return _liveTourRepository.GetEndedTours(tours);
        }

        public List<string> GetDates(string liveTour)
        {
            return _liveTourRepository.GetDates(liveTour);
        }

        public LiveTour GetByNameAndDate(string name, DateTime date)
        {
            return _liveTourRepository.GetByNameAndDate(name, date);
        }
    }
}
