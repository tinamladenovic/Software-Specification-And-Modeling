using FiveStarTours.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveStarTours.Interfaces
{
    public interface IKeyPointsRepository
    {
        List<KeyPoints> GetAll();
        KeyPoints Save(KeyPoints keyPoint);
        string GetById(int id);
        int NextId();
        List<string> GetAllNames();
    }
}
