using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApplication.Pages.SQLiteService.Entities
{
    [Table("SportTeams")]
    public class SportTeam
    {
        [PrimaryKey, AutoIncrement, Indexed]
        public int Id { get; set; }
        public string Name { get; set; }
        public string KindOfSport { get; set; }
    }
}
