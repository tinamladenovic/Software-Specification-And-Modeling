using FiveStarTours.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveStarTours.Interfaces
{
    public interface ILanguagesRepository
    {
        List<Language> GetAll();
        Language Save(Language language);
        int NextId();
        Language GetById(int id);
    }
}
