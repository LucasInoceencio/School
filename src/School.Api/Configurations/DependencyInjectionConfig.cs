using Microsoft.AspNetCore.Identity;
using School.Application.Interfaces;
using School.Application.Services;
using School.Domain.Interfaces;
using School.Identity.Services;
using School.Indentity.Data;
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
        services.AddDefaultIdentity<IdentityUser>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<IdentityDataContext>()
            .AddDefaultTokenProviders();
            
        services.AddScoped<IIdentityService, IdentityService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<SchoolContext>();
        //services.AddScoped<IdentityDataContext>();
    }
}