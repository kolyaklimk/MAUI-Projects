using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using MyApplication.Pages.SQLite.Entities;

namespace MyApplication.Pages.SQLite.Services
{
    internal class SQLiteService : IDbService
    {
        private SQLiteConnection Db =
            new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                "MyData.db"),
                SQLite.SQLiteOpenFlags.ReadWrite |
                SQLite.SQLiteOpenFlags.Create |
                SQLite.SQLiteOpenFlags.SharedCache);

        public IEnumerable<SportTeam> GetAllSportTeams()
        {
            return Db.Table<SportTeam>().ToList();
        }
        public IEnumerable<Member> GetSportTeamMembers(int id)
        {
            return Db.Table<Member>().Where(objId => objId.SportTeamId == id).ToList();
        }
        public void Init()
        {
            Db.DropTable<SportTeam>();
            Db.DropTable<Member>();

            Db.CreateTable<SportTeam>();
            Db.CreateTable<Member>();

            Db.Insert(new SportTeam { Id = 1, Name = "Los Angeles Lakers", KindOfSport = "Basketball" });
            Db.Insert(new SportTeam { Id = 2, Name = "Chicago Bulls", KindOfSport = "Basketball" });
            Db.Insert(new SportTeam { Id = 3, Name = "Dynamo", KindOfSport = "Hockey" });

            Db.Insert(new Member { MemberId = 1, Name = "M. Bamba", SportTeamId = 1 });
            Db.Insert(new Member { MemberId = 2, Name = "M. Beasley", SportTeamId = 1 });
            Db.Insert(new Member { MemberId = 3, Name = "T. Brown Jr.", SportTeamId = 1 });
            Db.Insert(new Member { MemberId = 4, Name = "S. Pippen Jr.", SportTeamId = 1 });
            Db.Insert(new Member { MemberId = 5, Name = "D. Reed", SportTeamId = 1 });
            Db.Insert(new Member { MemberId = 6, Name = "C. Swider", SportTeamId = 1 });

            Db.Insert(new Member { MemberId = 1, Name = "L. Ball", SportTeamId = 2 });
            Db.Insert(new Member { MemberId = 2, Name = "A. Caruso", SportTeamId = 2 });
            Db.Insert(new Member { MemberId = 3, Name = "G. Dragic", SportTeamId = 2 });
            Db.Insert(new Member { MemberId = 4, Name = "J. Green", SportTeamId = 2 });
            Db.Insert(new Member { MemberId = 5, Name = "D. Jones Jr.", SportTeamId = 2 });

            Db.Insert(new Member { MemberId = 1, Name = "Alistrov Vladimir", SportTeamId = 3 });
            Db.Insert(new Member { MemberId = 2, Name = "Barberio Mark", SportTeamId = 3 });
            Db.Insert(new Member { MemberId = 3, Name = "Gilmour John", SportTeamId = 3 });
            Db.Insert(new Member { MemberId = 4, Name = "Gorbunov Roman S.", SportTeamId = 3 });
            Db.Insert(new Member { MemberId = 5, Name = "Demkov Artyom", SportTeamId = 3 });
            Db.Insert(new Member { MemberId = 6, Name = "Deryabin Dmitry", SportTeamId = 3 });
            Db.Insert(new Member { MemberId = 7, Name = "Dushak Joseph", SportTeamId = 3 });
            Db.Insert(new Member { MemberId = 8, Name = "Zvyagin Stepan", SportTeamId = 3 });
        }
    }
}
