using School.Application.Interfaces;
using School.Application.Services;
using School.Domain.Interfaces;
using School.Infrastructure.Context;
using School.Infrastructure.Repository;
using School.Infrastructure.Uow;

namespace School.Api.Configurations;

public static class DependencyInjectionConfig
{
    public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddScoped<IPersonService, PersonService>();

        // Infrastructure
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<SchoolContext>();
        //services.AddScoped<IdentityDataContext>();
    }
}