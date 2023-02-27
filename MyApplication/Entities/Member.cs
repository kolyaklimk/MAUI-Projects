using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApplication.Entities
{
    [Table("Members")]
    public class Member
    {
        [PrimaryKey, AutoIncrement, Indexed]
        [Column("Id")]
        public int MemberId { get; set; }
        public string Name { get; set; }
        [Indexed]
        public int SportTeamId { get; set; }
    }
}
