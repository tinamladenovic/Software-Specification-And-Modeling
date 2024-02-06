using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveStarTours.Interfaces;
using FiveStarTours.Model;

namespace FiveStarTours.Services
{
    public class ToursService
    {
        private IToursRepository _toursRepository;
        public ToursService()
        {
            _toursRepository = Injector.Injector.CreateInstance<IToursRepository>();
        }

        public List<Tour> GetAll()
        {
            return _toursRepository.GetAll();
        }

        public Tour Save(Tour tour)
        {
            return _toursRepository.Save(tour);
        }

        public int NextId()
        {
            return _toursRepository.NextId();
        }

        public Tour GetById(int id)
        {
            return _toursRepository.GetById(id);
        }

        public List<Tour> GetAllByDate(DateTime date, User user)
        {
            return _toursRepository.GetAllByDate(date, user);
        }

        public List<Tour> GetByUser(User user)
        {
            return _toursRepository.GetByUser(user);
        }

        public void DeleteByDate(Tour tour)
        {
            _toursRepository.DeleteByDate(tour);
        }

        public List<string> GetNamesById(List<int> ids)
        {
            return _toursRepository.GetNamesById(ids);
        }

        public int GetByName(string name)
        {
            return _toursRepository.GetByName(name);
        }

        public List<DateTime> GetAllDates()
        {
            List<DateTime> dates = new List<DateTime>();

            foreach (Tour tour in _toursRepository.GetAll())
            {
                foreach (var date in tour.Beginning)
                {
                    dates.Add(date);
                }
            }

            return dates;
        }
    }
}
