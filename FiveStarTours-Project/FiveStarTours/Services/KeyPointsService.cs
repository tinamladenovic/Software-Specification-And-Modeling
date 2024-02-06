using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveStarTours.Interfaces;
using FiveStarTours.Model;

namespace FiveStarTours.Services
{
    public class KeyPointsService
    {
        private IKeyPointsRepository _keyPointsRepository;

        public KeyPointsService()
        {
            _keyPointsRepository = Injector.Injector.CreateInstance<IKeyPointsRepository>();
        }

        public List<KeyPoints> GetAll()
        {
            return _keyPointsRepository.GetAll();
        }

        public KeyPoints Save(KeyPoints keyPoint)
        {
            return _keyPointsRepository.Save(keyPoint);
        }

        public string GetById(int id)
        {
            return _keyPointsRepository.GetById(id);
        }

        public int NextId()
        {
            return _keyPointsRepository.NextId();
        }
        public List<string> GetAllNames()
        {
            return _keyPointsRepository.GetAllNames();
        }
    }
}
