// Modeled on CommentRepository from InitialProject

using System.Collections.Generic;
using System.Linq;
using FiveStarTours.Interfaces;
using FiveStarTours.Model;
using FiveStarTours.Serializer;
using FiveStarTours.View;

namespace FiveStarTours.Repository
{
    public class KeyPointsRepository : IKeyPointsRepository
    {
        private const string FilePath = "../../../Resources/Data/keypoints.csv";

        private readonly Serializer<KeyPoints> _serializer;

        private List<KeyPoints> _keyPoints;

        public KeyPointsRepository()
        {
            _serializer = new Serializer<KeyPoints>();
            _keyPoints = _serializer.FromCSV(FilePath);
        }

        public List<KeyPoints> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public KeyPoints Save(KeyPoints keyPoint)
        {
            keyPoint.Id = NextId();
            _keyPoints = _serializer.FromCSV(FilePath);
            _keyPoints.Add(keyPoint);
            _serializer.ToCSV(FilePath, _keyPoints);
            return keyPoint;
        }
        
        public string GetById(int id)
        {
            string result = null;
            _keyPoints = GetAll();
            foreach (KeyPoints keyPoints in _keyPoints)
            {
                if (keyPoints.Id == id)
                {
                   result = keyPoints.Name;
                }
            }
            return result;
        }

        public int NextId()
        {
            _keyPoints = _serializer.FromCSV(FilePath);
            if (_keyPoints.Count < 1)
            {
                return 1;
            }
            return _keyPoints.Max(kp => kp.Id) + 1;
        }
        public List<string> GetAllNames()
        {
            List<KeyPoints> keyPoints = new List<KeyPoints>();
            keyPoints = GetAll();
            List<string> result = new List<string>();
            foreach (var keyPoint in keyPoints)
            {
                result.Add(keyPoint.Name);
            }
            return result;
        }
    }
}
