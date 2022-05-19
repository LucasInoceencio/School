using FluentValidation;
using FluentValidation.AspNetCore;
using School.Domain.Entities;
using School.Domain.Validators;

namespace School.Api.Configurations;

public static class FluentValidationConfig
{
    public static void AddValidatorConfiguration(this IServiceCollection services)
    {
        services.AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>());
        services.AddTransient<IValidator<Person>, PersonValidator>();
    }
}