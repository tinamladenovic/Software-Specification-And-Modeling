using FiveStarTours.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FiveStarTours.Model
{
    public class User : ISerializable
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public bool Super { get; set; }


        public User() { }

        public User(int id, string username, string password, string role, string name, int age)
        {
            Id = id;
            Username = username;
            Password = password;
            Role = role;
            Name = name;
            Age = age;
            Super = false;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Username, Password, Role, Name, Age.ToString(), Super.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Username = values[1];
            Password = values[2];
            Role = values[3];
            Name = values[4];
            Age = Convert.ToInt32(values[5]);
            Super = Convert.ToBoolean(values[6]);
        }


    }
}
