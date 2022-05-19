using School.Domain.Entities.Core;

namespace School.Domain.Entities;

public sealed class StudentCourse : Entity
{
    public int StudentId { get; set; }
    public Student Student { get; set; }
    public int CourseId { get; set; }
    public Course Course { get; set; }
    public DateTime InitialDate { get; set; } = DateTime.Now;
    public DateTime? FinalDate { get; set; }
}