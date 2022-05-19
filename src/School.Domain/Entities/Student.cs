using School.Domain.Entities.Core;

namespace School.Domain.Entities;

public sealed class Student : Entity
{
    public bool Active { get; set; }
    public int PersonId { get; set; }
    public Person Person { get; set; }
    public string RegistrationNumber { get; set; }

    public Student(int personId, string registrationNumber)
    {
        Active = true;
        PersonId = personId;
        RegistrationNumber = registrationNumber;
    }
}