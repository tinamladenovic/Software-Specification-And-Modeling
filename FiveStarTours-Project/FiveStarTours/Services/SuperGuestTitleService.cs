using FiveStarTours.Interfaces;
using FiveStarTours.Model;
using FiveStarTours.Repository;
using FiveStarTours.Serializer;
using FiveStarTours.View.Traveler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;


namespace FiveStarTours.Services
{
    public class SuperGuestTitleService
    {
        private ISuperGuestTitleRepository _superGuestTitleRepository;

        public SuperGuestTitleService()
        {
            _superGuestTitleRepository = Injector.Injector.CreateInstance<ISuperGuestTitleRepository>();
        }

        public List<SuperGuestTitle> GetAll()
        {
            return _superGuestTitleRepository.GetAll();
        }

        public SuperGuestTitle Save(SuperGuestTitle super)
        {
            return _superGuestTitleRepository.Save(super);
        }

        public int NextId()
        {
            return NextId();
        }

        public SuperGuestTitle GetById(int id)
        {
            return _superGuestTitleRepository.GetById(id);
        }

        public SuperGuestTitle Update(SuperGuestTitle super)
        {
            return _superGuestTitleRepository.Update(super);
        }
        public SuperGuestTitle GetValidTitleForUserId(int userId)
        {
            return _superGuestTitleRepository.GetValidTitleForUserId(userId);
        }
    }
}
