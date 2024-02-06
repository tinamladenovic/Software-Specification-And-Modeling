using FiveStarTours.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveStarTours.Interfaces
{
    public interface IUserRepository
    {
        public User GetByUsername(string username);
        public User GetByNameSurname(string name);
        public List<User> GetAll();
        public User GetById(int id);
        public int FindIdByName(String name);
        public User Update(User user);
    }
}
