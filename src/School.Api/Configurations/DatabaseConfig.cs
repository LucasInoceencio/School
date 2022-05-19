using Microsoft.EntityFrameworkCore;
using School.Infrastructure.Persistence.Context;

namespace School.Api.Configurations;

public static class DatabaseConfig
{
    public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddDbContext<SchoolContext>(
        context =>
        {
            context.UseNpgsql(
                configuration.GetConnectionString("PostgreSqlConnection"),
                b => b.MigrationsAssembly("School.Infrastructure"));
        });

        // Other providers
        //services.AddDbContext<EscolaContext>(
        //    context =>
        //    {
        //        context.UseSqlServer(configuration.GetConnectionString("SqlServer"), b => b.MigrationsAssembly("Escola.Infraestrutura"));
        //    });

        //ervices.AddDbContext<EscolaContext>(
        //    context =>
        //    {
        //        context.UseOracle(configuration.GetConnectionString("Oracle"), b => b.MigrationsAssembly("Escola.Infraestrutura"));
        //    });
    }
}