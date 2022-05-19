using Microsoft.Extensions.DependencyInjection;
using School.Application.Interfaces;
using School.Application.Services;
using School.Domain.Interfaces;
using School.Infrastructure.Persistence.Context;
using School.Infrastructure.Persistence.Repository;
using School.Infrastructure.Persistence.Uow;

namespace School.Infrastructure.Ioc;

public class NativeInjectorBootStrapper
{
    public static void RegisterServices(IServiceCollection services)
    {
        // Application
        services.AddScoped<IPersonService, PersonService>();

        // Infrastructure
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<SchoolContext>();
    }
}