using FluentValidation;
using School.Domain.Entities;

namespace School.Domain.Validators;

public class PersonValidator : AbstractValidator<Person>
{
    public PersonValidator()
    {
        RuleFor(x => x.Name).NotEmpty().NotNull().MinimumLength(3);
        RuleFor(x => x.Lastname).NotEmpty().NotNull().MinimumLength(3);
        RuleFor(x => x.Cpf).NotNull();
        RuleFor(x => x.Cpf.IsValid).NotEqual(false);
        RuleFor(x => x.Email).NotNull();
        RuleFor(x => x.Email.IsValid).NotEqual(false);
        RuleFor(x => x.BirthDate).GreaterThanOrEqualTo(new DateTime(1900, 01, 01));
        RuleFor(x => x.Age).LessThan(111);
    }
}