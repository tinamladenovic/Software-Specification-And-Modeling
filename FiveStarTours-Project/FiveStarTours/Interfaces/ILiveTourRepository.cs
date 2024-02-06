using FiveStarTours.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveStarTours.Interfaces
{
    public interface ILiveTourRepository
    {
        List<LiveTour> GetAll();
        LiveTour Save(LiveTour liveTour);
        int NextId();
        void FindIdAndSave(LiveTour liveTour, int idLiveTour);
        public List<Tour> GetEndedTours(List<Tour> tours);
        public List<string> GetDates(string liveTour);
        public LiveTour GetByNameAndDate(string name, DateTime date);
    }
}
