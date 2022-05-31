using Serilog;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;

namespace School.Api.Configurations;

public static class LogsConfig
{
    public static void ConfigureLogs()
    {
        // Get the environment which the application is running on
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        // Get the configuration 
        var configuration = ApiConfig.GetConfigurationBuilder();

        // Create Logger
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            //.Enrich.WithExceptionDetails() // Adds details exception
            .WriteTo.Debug()
            .WriteTo.Console()
            .WriteTo.Elasticsearch(ConfigureELS(configuration, env))
            .CreateLogger();
    }

    public static ElasticsearchSinkOptions ConfigureELS(IConfigurationRoot configuration, string env)
    {
        return new ElasticsearchSinkOptions(new Uri(configuration["Elasticsearch:Uri"]))
        {
            AutoRegisterTemplate = true,
            IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower()}-{env.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}",
        };
    }
}