using FluentMigrator.Runner;
using FluentMigrator.Runner.VersionTableInfo;
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
                .WithVersionTable<CustomVersionTable>()
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

internal class CustomVersionTable : IVersionTableMetaData
{
    public object ApplicationContext { get; set; }
    public string AppliedOnColumnName => "applied_on";
    public string ColumnName => "version";
    public string DescriptionColumnName => "description";
    public bool OwnsSchema => true;
    public string SchemaName => "public";
    public string TableName => "version_info";
    public string UniqueIndexName => "ix_version_info";
}

internal static class MigrationExtensions
{
    public static IMigrationRunnerBuilder WithVersionTable<T>(
        this IMigrationRunnerBuilder builder)
        where T : class, IVersionTableMetaData
    {
        builder.Services.AddScoped<IVersionTableMetaData, T>();
        return builder;
    }
}