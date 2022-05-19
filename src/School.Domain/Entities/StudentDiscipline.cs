using School.Domain.Entities.Core;

namespace School.Domain.Entities;

public sealed class StudentDiscipline : Entity
{
    public int StudentId { get; set; }
    public Student Student { get; set; }
    public int DisciplineId { get; set; }
    public Discipline Discipline { get; set; }
    public DateTime InitialDate { get; set; } = DateTime.Now;
    public DateTime? FinalDate { get; set; }
    public int? Grade { get; set; }
}