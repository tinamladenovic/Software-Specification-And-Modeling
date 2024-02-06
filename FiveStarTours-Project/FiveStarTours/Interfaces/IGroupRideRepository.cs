using FiveStarTours.Model;
using FiveStarTours.Repository;
using FiveStarTours.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveStarTours.Interfaces
{
    public interface IGroupRideRepository
    {
        List<GroupRide> GetAll();
        GroupRide Save(GroupRide reserved);

        int NextId();
    }
}
