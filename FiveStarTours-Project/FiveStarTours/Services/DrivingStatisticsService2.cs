using FiveStarTours.Interfaces;
using FiveStarTours.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveStarTours.Services
{
    public class DrivingStatisticsService2
    {
        private IDrivingStatisticsRepository2 _drivingStatisticsRepository2;

        public DrivingStatisticsService2()
        {
            _drivingStatisticsRepository2 = Injector.Injector.CreateInstance<IDrivingStatisticsRepository2>();
        }

        public List<DrivingStatisticsData2> GetAll()
        {
            return _drivingStatisticsRepository2.GetAll();
        }

        public int NextId()
        {
            return NextId();
        }

        public int FindIdByYear(String year)
        {
            return _drivingStatisticsRepository2.FindIdByYear(year);
        }

        public List<string> GetAllYears()
        {
            return _drivingStatisticsRepository2.GetAllYears();

        }
    }
}
