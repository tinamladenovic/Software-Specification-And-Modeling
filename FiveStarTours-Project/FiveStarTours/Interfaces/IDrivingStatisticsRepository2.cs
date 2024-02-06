using FiveStarTours.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveStarTours.Interfaces
{
    public interface IDrivingStatisticsRepository2
    {
        List<DrivingStatisticsData2> GetAll();
        int NextId();
        int FindIdByYear(String year);
        List<string> GetAllYears();
    }
}
