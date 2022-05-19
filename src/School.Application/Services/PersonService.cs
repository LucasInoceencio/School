using AutoMapper;
using FluentValidation;
using School.Application.Interfaces;
using School.Application.ViewModels;
using School.Domain.Entities;
using School.Domain.Interfaces;
using School.Domain.Validators;

namespace School.Application.Services;

public class PersonService : IPersonService
{
    private readonly IMapper _mapper;
    private readonly IPersonRepository _personRepository;
    private readonly IValidator<Person> validator = new PersonValidator();

    public PersonService(
        IMapper mapper,
        IPersonRepository personRepository)
    {
        _mapper = mapper;
        _personRepository = personRepository;
    }

    public void Add(PersonViewModel person)
    {
        var personToAdd = _mapper.Map<Person>(person);

        var validationResult = validator.Validate(personToAdd);
        if (!validationResult.IsValid)
        {
            var erros = validationResult.Errors.Select(sl => sl.ErrorMessage).ToArray();
            var errosString = string.Join(",", erros);
            throw new ArgumentException($"Informações inconsistentes.{Environment.NewLine}{errosString}");
        }
        _personRepository.Add(personToAdd);
    }

    public async Task<PersonViewModel> GetById(int id)
    {
        return _mapper.Map<PersonViewModel>(await _personRepository.GetById(id));
    }

    public async Task<IEnumerable<PersonViewModel>> GetAll()
    {
        return _mapper.Map<IEnumerable<PersonViewModel>>(await _personRepository.GetAll());
    }

    public void Remove(PersonViewModel person)
    {
        var personToRemove = _mapper.Map<Person>(person);
        _personRepository.Remove(personToRemove);
    }

    public void Update(PersonViewModel person)
    {
        var personToUpdate = _mapper.Map<Person>(person);
        _personRepository.Update(personToUpdate);
    }
}