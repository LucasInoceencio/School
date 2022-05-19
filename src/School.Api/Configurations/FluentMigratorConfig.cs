using FluentMigrator.Runner;
using School.Infrastructure.Persistence.Migrations;

namespace School.Api.Configurations;

public static class FluentMigratorConfig
{
    public static void AddFluentMigrator(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddFluentMigratorCore()
           .ConfigureRunner(c =>
                c.AddPostgres11_0()
                .WithGlobalConnectionString(configuration.GetConnectionString("PostgreSqlConnection"))
                .ScanIn(typeof(PersonMigrator).Assembly).For.Migrations())
            .AddLogging(lg => lg.AddFluentMigratorConsole())
            .BuildServiceProvider(false);

    }

    public static void UpdateDatabase(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var runner = scope.ServiceProvider.GetService<IMigrationRunner>();
        runner.MigrateUp();
    }
}