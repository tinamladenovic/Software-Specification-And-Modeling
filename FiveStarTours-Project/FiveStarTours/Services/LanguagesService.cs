using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveStarTours.Interfaces;
using FiveStarTours.Model;

namespace FiveStarTours.Services
{
    public class LanguagesService
    {
        private ILanguagesRepository _languagesRepository;
        public LanguagesService()
        {
            _languagesRepository = Injector.Injector.CreateInstance<ILanguagesRepository>();
        }

        public List<Language> GetAll()
        {
            return _languagesRepository.GetAll();
        }

        public Language Save(Language language)
        {
            return _languagesRepository.Save(language);
        }

        public int NextId()
        {
            return _languagesRepository.NextId();
        }

       
    }
}
