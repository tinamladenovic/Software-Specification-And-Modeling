using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveStarTours.Interfaces;
using FiveStarTours.Model;
using FiveStarTours.Serializer;

namespace FiveStarTours.Repository
{
    public class LiveTourRepository : ILiveTourRepository
    {
        private const string FilePath = "../../../Resources/Data/livetours.csv";

        private readonly Serializer<LiveTour> _serializer;

        private List<LiveTour> _liveTours;

        public LiveTourRepository()
        {
            _serializer = new Serializer<LiveTour>();
            _liveTours = _serializer.FromCSV(FilePath);
        }

        public List<LiveTour> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public LiveTour Save(LiveTour liveTour)
        {
            liveTour.Id = NextId();
            _liveTours = _serializer.FromCSV(FilePath);
            _liveTours.Add(liveTour);
            _serializer.ToCSV(FilePath, _liveTours);
            return liveTour;
        }

        public int NextId()
        {
            _liveTours = _serializer.FromCSV(FilePath);
            if (_liveTours.Count < 1)
            {
                return 1;
            }
            return _liveTours.Max(lt => lt.Id) + 1;
        }

        public void FindIdAndSave(LiveTour liveTour, int idLiveTour)
        {
            List<LiveTour> liveToursList = new List<LiveTour>();
            liveToursList = GetAll();
            var livetour = liveToursList.ElementAt(idLiveTour-1);
            if(livetour != null)
            {
                livetour = liveTour;
                liveToursList[idLiveTour-1] = livetour;
            }

            _serializer.ToCSV(FilePath, liveToursList);
        }

        public List<string> GetEndedTours(List<Tour> tours)
        {
            List<string> result = new List<string>();
            foreach (LiveTour liveTour in GetAll())
            {
                foreach(var tour in tours)
                {
                    if (liveTour.Ended && tour.Id == liveTour.IdTour)
                    {
                        result.Add(liveTour.Name);
                    }
                }
            }
            return result;
        }

        public List<string> GetDates(string liveTour)
        {
            var result = new List<string>();
            foreach(LiveTour lt in GetAll())
            {
                if(lt.Name == liveTour)
                {
                    result.Add(Convert.ToString(lt.Date));
                }
            }
            return result;
        }

        public LiveTour GetByNameAndDate(string name, DateTime date)
        {
            LiveTour result = new LiveTour();
            foreach(LiveTour lt in GetAll())
            {
                if(name == lt.Name && date == lt.Date)
                {
                    result = lt;
                }
            }
            return result;
        }
    }
}
