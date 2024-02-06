using FiveStarTours.Interfaces;
using FiveStarTours.Model;
using FiveStarTours.Repository;
using FiveStarTours.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveStarTours.Services
{
    public class GroupRideService
    {
        private IGroupRideRepository _groupRideRepository;
        public GroupRideService()
        {
            _groupRideRepository = Injector.Injector.CreateInstance<IGroupRideRepository>();
        }
        public List<GroupRide> GetAll()
        {
            return _groupRideRepository.GetAll();
        }

        public GroupRide Save(GroupRide groupRide)
        {
            return _groupRideRepository.Save(groupRide);
        }

        public int NextId()
        {
            return  _groupRideRepository.NextId();
        }
    }
}
