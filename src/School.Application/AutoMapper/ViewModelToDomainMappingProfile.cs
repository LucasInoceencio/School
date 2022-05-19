using AutoMapper;
using School.Application.ViewModels;
using School.Domain.Entities;

namespace School.Application.AutoMapper;

public class ViewModelToDomainMappingProfile : Profile
{
    public ViewModelToDomainMappingProfile()
    {
        CreateMap<PersonViewModel, Person>();
    }
}