using FiveStarTours.Interfaces;
using FiveStarTours.Model;
using FiveStarTours.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FiveStarTours.Repository
{
    public class UserRepository : IUserRepository
    {
        private const string FilePath = "../../../Resources/Data/users.csv";

        private readonly Serializer<User> _serializer;
        private static UserRepository instance = null;
        private List<User> _users;
        public static UserRepository GetInstace()
        {
            if (instance == null)
            {
                instance = new UserRepository();
            }
            return instance;
        }

        public UserRepository()
        {
            _serializer = new Serializer<User>();
            _users = _serializer.FromCSV(FilePath);
        }

        public User GetByUsername(string username)
        {
            _users = _serializer.FromCSV(FilePath);
            return _users.FirstOrDefault(u => u.Username == username);
        }

        public User GetByNameSurname(string name)
        {
            return _users.FirstOrDefault(u => u.Name == name);
        }


        public List<User> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public User GetById(int id)
        {
            _users = GetAll();
            foreach (User user in _users)
            {
                if (user.Id == id)
                {
                    return user;
                }
            }
            return null;
        }

        public int FindIdByName(String name)
        {
            _users = GetAll();
            int id = 0;
            foreach(User user in _users)
            {
                if(user.Name == name)
                {
                    id = user.Id;
                }
            }

            return id;
        }


        public User Update(User user)
        {
            _users = _serializer.FromCSV(FilePath);
            User current = _users.Find(c => c.Id == user.Id);

            if (current != null)
            {
                int index = _users.IndexOf(current);
                _users.Remove(current);
                _users.Insert(index, user);
                _serializer.ToCSV(FilePath, _users);
            }

            return user;
        }


    }
}
