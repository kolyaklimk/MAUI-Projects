using System.ComponentModel.DataAnnotations;

namespace _153504_Klimkovich.Domain.Entities;

public class Entity
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
}
