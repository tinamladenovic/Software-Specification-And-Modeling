// Modeled on CommentRepository from InitialProject

using System.Collections.Generic;
using System.Linq;
using FiveStarTours.Interfaces;
using FiveStarTours.Model;
using FiveStarTours.Serializer;

namespace FiveStarTours.Repository
{
    public class LanguagesRepository : ILanguagesRepository
    {
        private const string FilePath = "../../../Resources/Data/languages.csv";

        private readonly Serializer<Language> _serializer;

        private List<Language> _languages;

        public LanguagesRepository()
        {
            _serializer = new Serializer<Language>();
            _languages = _serializer.FromCSV(FilePath);
        }

        public List<Language> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Language Save(Language language)
        {
            language.Id = NextId();
            _languages = _serializer.FromCSV(FilePath);
            _languages.Add(language);
            _serializer.ToCSV(FilePath, _languages);
            return language;
        }

        public int NextId()
        {
            _languages = _serializer.FromCSV(FilePath);
            if (_languages.Count < 1)
            {
                return 1;
            }
            return _languages.Max(l => l.Id) + 1;
        }
        public Language GetById(int id)
        {
            return _languages.FirstOrDefault(l => l.Id == id);
        }
        
    }
}
