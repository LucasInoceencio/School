using School.Domain.Entities.Core;

namespace School.Domain.Entities;

public sealed class Teacher : Entity
{
    public bool Active { get; set; }
    public int PersonId { get; set; }
    public Person Person { get; set; }
    public string RegistrationNumber { get; set; }

    public Teacher(int personId, string registerAtSchool)
    {
        Active = true;
        PersonId = personId;
        RegistrationNumber = registerAtSchool;
    }
}