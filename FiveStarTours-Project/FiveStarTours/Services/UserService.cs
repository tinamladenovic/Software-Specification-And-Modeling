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
        private AccommodationReservationService _reservations;
        private ISuperGuestTitleRepository _superGuestTitleRepository;

        public UserService()
        {
            _userRepository = Injector.Injector.CreateInstance<IUserRepository>();
            _superGuestTitleRepository = Injector.Injector.CreateInstance<ISuperGuestTitleRepository>();
            _reservations = new AccommodationReservationService();
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
        public void CheckIfSuperGuest(User user)
        {
            SuperGuestTitle guestTitle = _superGuestTitleRepository.GetValidTitleForUserId(user.Id);
            if (guestTitle != null)
            {
                if(guestTitle.StartTime.AddYears(guestTitle.DurationYears) > DateTime.Now)
                {
                    guestTitle.Valid = false;
                    _superGuestTitleRepository.Update(guestTitle);
                }
                else
                {
                    return;
                }
            }
            int numberOfReservations = _reservations.CountReservationsInLastYear();
            if(numberOfReservations >= 10)
            {
                user.Super = true;
                SuperGuestTitle superGuestTitle = new SuperGuestTitle() {User=user, StartTime = DateTime.Now, DurationYears = 1, Points = 5, Valid = true};
                _superGuestTitleRepository.Save(superGuestTitle);
                
                
                Update(user);
            }
            else
            {
                user.Super = false;
                Update(user);
            }
        }
    }
}
