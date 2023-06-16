namespace _153504_Klimkovich.Domain.Entities;

public class SportTeam: Entity
{
    public string KindOfSport { get; set; }
    public ICollection<Member> Members { get; set; }
}
