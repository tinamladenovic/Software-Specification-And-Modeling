using FiveStarTours.Interfaces;
using FiveStarTours.Model;
using FiveStarTours.Serializer;
using FiveStarTours.View.Traveler;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveStarTours.Repository
{
    public class SuperGuestTitleRepository : ISuperGuestTitleRepository
    {
        private const string FilePath = "../../../Resources/Data/superGuest.csv";

        private readonly Serializer<SuperGuestTitle> _serializer;

        private static SuperGuestTitleRepository instance = null;

        private List<SuperGuestTitle> _super;
        public static SuperGuestTitleRepository GetInstace()
        {
            if (instance == null)
            {
                instance = new SuperGuestTitleRepository();
            }
            return instance;
        }
        public SuperGuestTitleRepository()
        {
            _serializer = new Serializer<SuperGuestTitle>();
            _super= _serializer.FromCSV(FilePath);
        }

        public List<SuperGuestTitle> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public SuperGuestTitle Save(SuperGuestTitle superGuest)
        {
           superGuest.Id = NextId();
            _super = _serializer.FromCSV(FilePath);
            _super.Add(superGuest);
            _serializer.ToCSV(FilePath, _super);
            return superGuest;
        }

        public int NextId()
        {
            _super = _serializer.FromCSV(FilePath);
            if (_super.Count < 1)
            {
                return 1;
            }
            return _super.Max(t => t.Id) + 1;
        }

        public SuperGuestTitle GetById(int id)
        {
            _super = GetAll();
            foreach (SuperGuestTitle superGuest in _super)
            {
                if (superGuest.Id == id)
                {
                    return superGuest;
                }
            }
            return null;
        }

        public SuperGuestTitle Update(SuperGuestTitle superGuest)
        {
            _super = _serializer.FromCSV(FilePath);
            SuperGuestTitle current = _super.Find(c => c.Id == superGuest.Id);
            int index = _super.IndexOf(current);
            _super.Remove(current);
            _super.Insert(index, superGuest);
            _serializer.ToCSV(FilePath, _super);
            return superGuest;
        }

        public SuperGuestTitle GetValidTitleForUserId(int userId)
        {
            foreach(SuperGuestTitle guestTitle in _super)
            {
                if(guestTitle.User.Id == userId && guestTitle.Valid)
                {
                    return guestTitle;
                }
            }
            return null;
        }
    }
}
