using FiveStarTours.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveStarTours.Repository;

namespace FiveStarTours.Interfaces
{
    public interface ISuperGuestTitleRepository
    {
        List<SuperGuestTitle> GetAll();
        SuperGuestTitle Save(SuperGuestTitle super);
        int NextId();
        SuperGuestTitle GetById(int id);
        SuperGuestTitle Update(SuperGuestTitle super);

        SuperGuestTitle GetValidTitleForUserId(int userId);
    }
}
