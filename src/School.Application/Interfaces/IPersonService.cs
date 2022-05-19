using School.Application.ViewModels;

namespace School.Application.Interfaces;

public interface IPersonService
{
    Task<IEnumerable<PersonViewModel>> GetAll();
    Task<PersonViewModel> GetById(int id);
    void Add(PersonViewModel person);
    void Update(PersonViewModel person);
    void Remove(PersonViewModel person);
}