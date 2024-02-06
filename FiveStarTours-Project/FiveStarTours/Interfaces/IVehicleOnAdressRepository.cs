using FiveStarTours.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveStarTours.Interfaces
{
    public interface IVehicleOnAdressRepository
    {
        List<OnAdress> GetAll();
        OnAdress Save(OnAdress newVehicleOnAdress);
        int NextId();
        List<OnAdress> GetAllFastDrive();
        int FastDriveCount();
    }
}
