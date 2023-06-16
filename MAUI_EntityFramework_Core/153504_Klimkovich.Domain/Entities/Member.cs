namespace _153504_Klimkovich.Domain.Entities;

public class Member: Entity
{
    public int Age { get; set; }
    public int Points { get; set; }
    public SportTeam SportTeam { get; set; }
}
