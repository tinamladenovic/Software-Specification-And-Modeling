using FiveStarTours.Interfaces;
using FiveStarTours.Model;
using FiveStarTours.Interfaces;
using FiveStarTours.Model;
using FiveStarTours.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveStarTours.Repository
{
    public class GroupRideRepository : IGroupRideRepository
    {
        private const string FilePath = "../../../Resources/Data/groupRide.csv";
        private readonly Serializer<GroupRide> _serializer;

        private List<GroupRide> _groupRideRepository;

        public GroupRideRepository()
        {
            _serializer = new Serializer<GroupRide>();
            _groupRideRepository = _serializer.FromCSV(FilePath);
        }

        public List<GroupRide> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public GroupRide Save(GroupRide groupRide)
        {
            groupRide.Id = NextId();
            _groupRideRepository = _serializer.FromCSV(FilePath);
            _groupRideRepository.Add(groupRide);
            _serializer.ToCSV(FilePath, _groupRideRepository);
            return groupRide;
        }

        public int NextId()
        {
            _groupRideRepository = _serializer.FromCSV(FilePath);
            if (_groupRideRepository.Count < 1)
            {
                return 1;
            }
            return _groupRideRepository.Max(t => t.Id) + 1;
        }
    }

}
