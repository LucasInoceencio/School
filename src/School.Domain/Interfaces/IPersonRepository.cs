using School.Domain.Entities;

namespace School.Domain.Interfaces;

public interface IPersonRepository
{
    Task<IEnumerable<Person>> GetAll();
    Task<Person> GetById(int id);
    void Add(Person person);
    void Update(Person person);
    void Remove(Person person);
}