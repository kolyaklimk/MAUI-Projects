using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using MyApplication.Pages.SQLiteService.Entities;

namespace MyApplication.Pages.SQLiteService.Services
{
    internal class SQLiteService : IDbService
    {
        private SQLiteConnection Db =
            new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "MyData.db"),
                SQLiteOpenFlags.ReadWrite |
                SQLiteOpenFlags.Create |
                SQLiteOpenFlags.SharedCache);

        public IEnumerable<SportTeam> GetAllSportTeams()
        {
            return Db.Table<SportTeam>().ToList();
        }
        public IEnumerable<Member> GetSportTeamMembers(int id)
        {
            return Db.Table<Member>().Where(objId => objId.SportTeamId == id).ToArray();
        }
        public void Init()
        {
            Db.DropTable<SportTeam>();
            Db.DropTable<Member>();

            Db.CreateTable<SportTeam>();
            Db.CreateTable<Member>();

            Db.Insert(new SportTeam { Id = 0, Name = "Los Angeles Lakers", KindOfSport = "Basketball" });
            Db.Insert(new SportTeam { Id = 1, Name = "Chicago Bulls", KindOfSport = "Basketball" });
            Db.Insert(new SportTeam { Id = 2, Name = "Dynamo", KindOfSport = "Hockey" });

            Db.Insert(new Member { MemberId = 0, Name = "M. Bamba", SportTeamId = 0 });
            Db.Insert(new Member { MemberId = 1, Name = "M. Beasley", SportTeamId = 0 });
            Db.Insert(new Member { MemberId = 2, Name = "T. Brown Jr.", SportTeamId = 0 });
            Db.Insert(new Member { MemberId = 3, Name = "S. Pippen Jr.", SportTeamId = 0 });
            Db.Insert(new Member { MemberId = 4, Name = "D. Reed", SportTeamId = 0 });
            Db.Insert(new Member { MemberId = 5, Name = "C. Swider", SportTeamId = 0 });

            Db.Insert(new Member { MemberId = 0, Name = "L. Ball", SportTeamId = 1 });
            Db.Insert(new Member { MemberId = 1, Name = "A. Caruso", SportTeamId = 1 });
            Db.Insert(new Member { MemberId = 2, Name = "G. Dragic", SportTeamId = 1 });
            Db.Insert(new Member { MemberId = 3, Name = "J. Green", SportTeamId = 1 });
            Db.Insert(new Member { MemberId = 4, Name = "D. Jones Jr.", SportTeamId = 1 });

            Db.Insert(new Member { MemberId = 0, Name = "Alistrov Vladimir", SportTeamId = 2 });
            Db.Insert(new Member { MemberId = 1, Name = "Barberio Mark", SportTeamId = 2 });
            Db.Insert(new Member { MemberId = 2, Name = "Gilmour John", SportTeamId = 2 });
            Db.Insert(new Member { MemberId = 3, Name = "Gorbunov Roman S.", SportTeamId = 2 });
            Db.Insert(new Member { MemberId = 4, Name = "Demkov Artyom", SportTeamId = 2 });
            Db.Insert(new Member { MemberId = 5, Name = "Deryabin Dmitry", SportTeamId = 2 });
            Db.Insert(new Member { MemberId = 6, Name = "Dushak Joseph", SportTeamId = 2 });
        }
    }
}
