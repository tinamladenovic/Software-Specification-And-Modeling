using FiveStarTours.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveStarTours.Interfaces
{
    public interface IDrivingStatisticsRepository
    {
        List<DrivingStatisticsData> GetAll();
        int NextId();
        public List<string> GetAllDrivingYears();
    }
}
