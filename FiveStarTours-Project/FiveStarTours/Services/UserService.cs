using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveStarTours.Interfaces;
using FiveStarTours.Model;

namespace FiveStarTours.Services
{
    public class UserService
    {
        private IUserRepository _userRepository;
        public UserService()
        {
            _userRepository = Injector.Injector.CreateInstance<IUserRepository>();
        }

        public User GetByUsername(string username)
        {
            return _userRepository.GetByUsername(username);
        }

        public User GetByNameSurname(string name)
        {
            return _userRepository.GetByNameSurname(name);
        }


        public List<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public User GetById(int id)
        {
            return _userRepository.GetById(id);
        }

        public int FindIdByName(String name)
        {
            return _userRepository.FindIdByName(name);
        }

        public User Update(User user)
        {
            return _userRepository.Update(user);
        }
    }
}
