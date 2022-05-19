using School.Domain.Entities.Core;

namespace School.Domain.Entities;

public sealed class Course : Entity
{
    public string Name { get; set; }
    public string MecRegister { get; set; }
    public IEnumerable<Discipline> Disciplines { get; set; }
}