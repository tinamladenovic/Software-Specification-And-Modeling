using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveStarTours.Interfaces;
using FiveStarTours.Model;
using FiveStarTours.Repository;

namespace FiveStarTours.Services
{
    public class AttendanceService
    {
        private IAttendanceRepository _attendanceRepository;

        public AttendanceService()
        {
            _attendanceRepository = Injector.Injector.CreateInstance<IAttendanceRepository>();
        }
        public List<Attendance> GetAll()
        {
            return _attendanceRepository.GetAll();
        }

        public Attendance Save(Attendance attendance)
        {
            return _attendanceRepository.Save(attendance);
        }

        public int NextId()
        {
            return _attendanceRepository.NextId();
        }

        public int GetAllLower(int id, List<User> users)
        {
            return _attendanceRepository.GetAllLower(id, users);
        }

        public int GetAllBetween(int id, List<User> users)
        {
            return _attendanceRepository.GetAllBetween(id, users);
        }

        public int GetAllAbove(int id, List<User> users)
        {
            return _attendanceRepository.GetAllAbove(id, users);
        }

        public int GetAllById(int id, List<User> users)
        {
            return _attendanceRepository.GetAllById(id, users);
        }

        public int GetMostVisitedTour(List<Tour> tours)
        {
            return _attendanceRepository.GetMostVisitedTour(tours);
        }

        public List<Attendance> GetAllByTours(List<Tour> tours)
        {
            return _attendanceRepository.GetAllByTours(tours);
        }

        public string GetMostVisitedByYear(DateTime date, List<Tour> tours)
        {
            return _attendanceRepository.GetMostVisitedByYear(date, tours);
        }
        public List<int> GetVisitedTours(int id)
        {
            return _attendanceRepository.GetVisitedTours(id);
        }
    }
}
