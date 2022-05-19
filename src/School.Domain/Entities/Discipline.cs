using School.Domain.Entities.Core;

namespace School.Domain.Entities;

public sealed class Discipline : Entity
{
    public string Name { get; set; }
    public int Workload { get; set; }
    public int? DisciplinePrerequisiteId { get; set; }
    public Discipline DisciplinePrerequisite { get; set; }
    public int TeacherId { get; set; }
    public Teacher Teacher { get; set; }
    public int CourseId { get; set; }
    public Course Course { get; set; }
    public IEnumerable<StudentDiscipline> StudentsDisciplines { get; set; }
}