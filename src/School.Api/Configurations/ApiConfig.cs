namespace School.Api.Configurations;

public static class ApiConfig
{
    public static IConfigurationRoot GetConfigurationBuilder()
    {
        var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env}.json", optional: true)
                .Build();

        return configuration;
    }
}