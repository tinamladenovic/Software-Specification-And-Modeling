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
    public class AttendanceRepository : IAttendanceRepository
    {
        private const string FilePath = "../../../Resources/Data/attendance.csv";

        private readonly Serializer<Attendance> _serializer;

        private List<Attendance> _attendances;

        public AttendanceRepository()
        {
            _serializer = new Serializer<Attendance>();
            _attendances = _serializer.FromCSV(FilePath);
        }

        public List<Attendance> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        
        public Attendance Save(Attendance attendance)
        {
            attendance.Id = NextId();
            _attendances = _serializer.FromCSV(FilePath);
            _attendances.Add(attendance);
            _serializer.ToCSV(FilePath, _attendances);
            return attendance;
        }

        public int NextId()
        {
            _attendances = _serializer.FromCSV(FilePath);
            if (_attendances.Count < 1)
            {
                return 1;
            }
            return _attendances.Max(a => a.Id) + 1;
        }

        public int GetAllLower(int id, List<User> users)
        {
            int result = 0;
            List<Attendance> Attendances = new List<Attendance>();
            foreach (var attendance in GetAll())
            {
                if (attendance.IdLiveTour == id)
                {
                    Attendances.Add(attendance);
                }
            }
            List<User> visitors = new List<User>();
            foreach (var attendance in Attendances)
            {
                foreach (var user in users)
                {
                    if (attendance.IdVisitor == user.Id)
                    {
                        visitors.Add(user);
                    }
                }

            }
            foreach (var visitor in visitors)
            {
                if (visitor.Age < 18)
                {
                    result = result + 1;
                }
            }

            return result;
        }

        public int GetAllBetween(int id, List<User> users)
        {
            int result = 0;
            List<Attendance> Attendances = new List<Attendance>();
            _attendances = GetAll();
            foreach (var attendance in _attendances)
            {
                if (attendance.IdLiveTour == id)
                {
                    Attendances.Add(attendance);
                }
            }
            List<User> visitors = new List<User>();
            foreach (var attendance in Attendances)
            {
                foreach (var user in users)
                {
                    if (attendance.IdVisitor == user.Id)
                    {
                        visitors.Add(user);
                    }
                }
            }

            foreach (var visitor in visitors)
            {
                if (visitor.Age >= 18 && visitor.Age < 50)
                {
                    result = result + 1;
                }
            }

            return result;
        }

        public int GetAllAbove(int id, List<User> users)
        {
            int result = 0;
            List<Attendance> Attendances = new List<Attendance>();
            foreach (var attendance in GetAll())
            {
                if (attendance.IdLiveTour == id)
                {
                    Attendances.Add(attendance);
                }
            }
            List<User> visitors = new List<User>();
            foreach (var attendance in Attendances)
            {
                foreach (var user in users)
                {
                    if (attendance.IdVisitor == user.Id)
                    {
                        visitors.Add(user);
                    }
                }
            }

            foreach (var visitor in visitors)
            {
                if (visitor.Age >= 50)
                {
                    result = result + 1;
                }
            }

            return result;
        }

        public int GetAllById(int id, List<User> users)
        {
            int result = 0;
            List<Attendance> Attendances = new List<Attendance>();
            _attendances = GetAll();
            foreach (var attendance in _attendances)
            {
                if (attendance.IdLiveTour == id)
                {
                    Attendances.Add(attendance);
                }
            }
            List<User> visitors = new List<User>();
            foreach (var attendance in Attendances)
            {
                foreach (var user in users)
                {
                    if (attendance.IdVisitor == user.Id)
                    {
                        visitors.Add(user);
                    }
                }
            }

            foreach (var visitor in visitors)
            {

                result = result + 1;

            }

            return result;
        }

        public int  GetMostVisitedTour(List<Tour> tours)
        {
            int id;
            _attendances = GetAllByTours(tours);
            id = _attendances.GroupBy(a => a.IdTour).OrderByDescending(t => t.Count()).Select(mv => mv.Key).FirstOrDefault();
            return id;
        }

        public List<Attendance> GetAllByTours(List<Tour> tours)
        {
            _attendances = GetAll();
            List<Attendance> attendances = new List<Attendance>();
            foreach(var attendnace in _attendances)
            {
                foreach (var tour in tours)
                {
                    if(tour.Id == attendnace.IdTour)
                    {
                        attendances.Add(attendnace);
                    }
                }
            }
            return attendances;
        }

        public string GetMostVisitedByYear(DateTime date, List<Tour> tours)
        {
            string result = null;
            _attendances = GetAll();
            List<Attendance> attendances = new List<Attendance>();
            foreach (var attendance in _attendances)
            {
                if (attendance.Date.Year == date.Year)
                {
                    attendances.Add(attendance);
                }
            }
            if(attendances.Count == 0)
            {
                result = "There's no visited tours this year.";
            } else
            {
                int id = attendances.GroupBy(a => a.IdTour).OrderByDescending(t => t.Count()).Select(mv => mv.Key).FirstOrDefault();
                foreach (var tour in tours)
                {
                    if (tour.Id == id)
                    {
                        result = tour.Name;
                    }
                }
            }
            return result;
        }
        public List<int> GetVisitedTours(int id)
        {
            List<int> result = new List<int>();
            _attendances = GetAll();
            foreach(var attendance in _attendances)
            {
                if(attendance.IdVisitor == id)
                {
                    result.Add(attendance.IdTour);
                }
            }
            return result;
        }
    }
}
