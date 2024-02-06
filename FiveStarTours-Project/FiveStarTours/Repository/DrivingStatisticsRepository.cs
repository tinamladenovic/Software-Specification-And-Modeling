using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveStarTours.Interfaces;
using FiveStarTours.Model;
using FiveStarTours.Serializer;
using FiveStarTours.View.Driver;

namespace FiveStarTours.Repository
{
    public class DrivingStatisticsRepository : IDrivingStatisticsRepository
    {
        private const string FilePath = "../../../Resources/Data/drivingstatistics.csv";

        private readonly Serializer<DrivingStatisticsData> _serializer;

        private List<DrivingStatisticsData> _drivingStatisticsData;

        public DrivingStatisticsRepository()
        {
            _serializer = new Serializer<DrivingStatisticsData>();
            _drivingStatisticsData = _serializer.FromCSV(FilePath);
            
        }

        public List<DrivingStatisticsData> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public List<string> GetAllDrivingYears()
        {
            List<DrivingStatisticsData> drivingYear = GetAll();
            List<string> years = drivingYear.Select(l => l.DrivingYear).Distinct().ToList();
            return years;
        }
        public int NextId()
        {
            _drivingStatisticsData = _serializer.FromCSV(FilePath);
            if (_drivingStatisticsData.Count < 1)
            {
                return 1;
            }
            return _drivingStatisticsData.Max(l => l.Id) + 1;
        }
    }
}
