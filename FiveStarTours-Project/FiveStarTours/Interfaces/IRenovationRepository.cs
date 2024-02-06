using FiveStarTours.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveStarTours.Interfaces
{
    public interface IRenovationRepository
    {
        List<Renovations> GetAll();
        Renovations Save(Renovations renovation);

        int NextId();
        Renovations GetById(int id);
        void Delete(Renovations renovation);
        Renovations Update(Renovations renovation);
        
        bool IsAbleToCancel(int renovationId);
        void CancelRenovation(Renovations renovation);
        public void SetToFalse(Renovations renovation);


    }
}
