using FiveStarTours.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveStarTours.Interfaces
{
    public interface IToursRepository
    {
        List<Tour> GetAll();
        Tour Save(Tour tour);
        int NextId();
        Tour GetById(int id);
        List<Tour> GetAllByDate(DateTime date, User user);
        List<Tour> GetByUser(User user);
        void DeleteByDate(Tour tour);
        public List<string> GetNamesById(List<int> ids);
        public int GetByName(string name);

    }
}
