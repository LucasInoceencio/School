using School.Domain.Entities.Core;
using School.Domain.ValueObjects;

namespace School.Domain.Entities;

public sealed class Person : Entity
{
    public bool Active { get; set; }
    public string Name { get; set; }
    public string Lastname { get; set; }
    public DateTime BirthDate { get; set; }
    public int Age
    {
        get
        {
            var actualDate = DateTime.Now;
            var age = actualDate.Year - BirthDate.Year;

            if (actualDate.Month < BirthDate.Month)
            {
                age--;
            }

            return age;
        }
    }
    public Cpf Cpf { get; set; }
    public Email Email { get; set; }
    public char Type { get; set; } = 'F';
    public string Memo { get; set; }
    public string FullName =>
        $"{Name} {Lastname}";

    public Person(string name, string lastName, DateTime birthDate, Cpf cpf, Email email)
    {
        Active = true;
        Name = name;
        Lastname = lastName;
        BirthDate = birthDate;
        Cpf = cpf;
        Email = email;
    }

    protected Person() { }
}