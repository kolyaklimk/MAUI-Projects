using MyApplication.Pages.SQLiteService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApplication.Pages.SQLiteService.Services
{
    public interface IDbService
    {
        IEnumerable<SportTeam> GetAllSportTeams();
        IEnumerable<Member> GetSportTeamMembers(int id);
        void Init();
    }
}
