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
    public class DrivingStatisticsService
    {
        private IDrivingStatisticsRepository _drivingStatisticsRepository;

        public DrivingStatisticsService()
        {
            _drivingStatisticsRepository = Injector.Injector.CreateInstance<IDrivingStatisticsRepository>();
        }

        public List<DrivingStatisticsData> GetAll()
        {
            return _drivingStatisticsRepository.GetAll();
        }

        public List<string> GetAllDrivingYears()
        {
            return _drivingStatisticsRepository.GetAllDrivingYears();
        }

        public int NextId()
        {
            return NextId();
        }
    }
}
