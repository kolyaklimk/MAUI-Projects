using _153504_Klimkovich.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace _153504_Klimkovich.Persistense.Data;

// https://metanit.com/sharp/efcore/2.10.php
public class AppDbContext: DbContext
{
    DbContextOptions<AppDbContext> _db;
    public DbSet<SportTeam> SportTeams { get; set; }
    public DbSet<Member> Members { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions)
    {
        Database.EnsureCreated();
    }
}
