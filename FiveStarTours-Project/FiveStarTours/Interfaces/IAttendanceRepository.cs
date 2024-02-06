using FiveStarTours.Model;
using FiveStarTours.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveStarTours.Interfaces
{
    public interface IAttendanceRepository
    {
        public List<Attendance> GetAll();
        public Attendance Save(Attendance attendance);
        public int NextId();
        public int GetAllLower(int id, List<User> users);
        public int GetAllBetween(int id, List<User> users);
        public int GetAllAbove(int id, List<User> users);
        public int GetAllById(int id, List<User> users);
        public int GetMostVisitedTour(List<Tour> tours);
        public List<Attendance> GetAllByTours(List<Tour> tours);
        public string GetMostVisitedByYear(DateTime date, List<Tour> tours);
        public List<int> GetVisitedTours(int id);
    }
}
