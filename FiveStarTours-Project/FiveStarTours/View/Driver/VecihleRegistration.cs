using FiveStarTours.Model;
using System.Collections.Generic;

namespace FiveStarTours.View.VehicleRegistration
{
    internal class VecihleRegistration : VehicleRegistration
    {
        private int id;
        private Location location;
        private List<int> languageIds;
        private List<Language> languageList;
        private object maxPersonNumber;
        private List<string> imageUrlsList;

        public VecihleRegistration(int id, Location location, List<int> languageIds, List<Language> languageList, object maxPersonNumber, List<string> imageUrlsList)
        {
            this.id = id;
            this.location = location;
            this.languageIds = languageIds;
            this.languageList = languageList;
            this.maxPersonNumber = maxPersonNumber;
            this.imageUrlsList = imageUrlsList;
        }
    }
}