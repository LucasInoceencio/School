using AutoMapper;
using School.Application.ViewModels;
using School.Domain.Entities;

namespace School.Application.AutoMapper;

public class DomainToViewModelMappingProfile : Profile
{
    public DomainToViewModelMappingProfile()
    {
        CreateMap<Person, PersonViewModel>();
    }
}