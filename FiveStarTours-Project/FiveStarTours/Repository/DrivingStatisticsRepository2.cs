using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveStarTours.Interfaces;
using FiveStarTours.Model;
using FiveStarTours.Serializer;
using FiveStarTours.View.VehicleOnAdress;

namespace FiveStarTours.Repository
{
    public class DrivingStatisticsRepository2 : IDrivingStatisticsRepository2
    {
        private const string FilePath = "../../../Resources/Data/drivingstatistics2.csv";

        private readonly Serializer<DrivingStatisticsData2> _serializer;

        private List<DrivingStatisticsData2> _drivingStatisticsData2;

        public DrivingStatisticsRepository2()
        {
            _serializer = new Serializer<DrivingStatisticsData2>();
            _drivingStatisticsData2 = _serializer.FromCSV(FilePath);

        }

        public List<DrivingStatisticsData2> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        
        public int NextId()
        {
            _drivingStatisticsData2 = _serializer.FromCSV(FilePath);
            if (_drivingStatisticsData2.Count < 1)
            {
                return 1;
            }
            return _drivingStatisticsData2.Max(l => l.Id) + 1;
        }

        public int FindIdByYear(String year)
        {
            _drivingStatisticsData2 = GetAll();
            int id = 0;
            foreach (DrivingStatisticsData2 drivingstatisticsdata2 in _drivingStatisticsData2)
            {
                if (drivingstatisticsdata2.DrivingYear2 == year)
                {
                    id = drivingstatisticsdata2.Id;
                }
            }

            return id;
        }

        public List<string> GetAllYears()
        {
            List<DrivingStatisticsData2> drivingStatisticsData2 = GetAll();
            List<string> years = drivingStatisticsData2.Select(l => l.DrivingYear2).Distinct().ToList();
            return years;
        }
    }

}
