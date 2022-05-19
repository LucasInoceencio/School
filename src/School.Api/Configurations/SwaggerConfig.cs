using Microsoft.OpenApi.Models;

namespace School.Api.Configurations;

public static class SwaggerConfig
{
    public static void AddSwaggerConfiguration(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));

        services.AddSwaggerGen(s =>
        {
            s.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Escola Project",
                Description = "Escola API Swagger",
                Contact = new OpenApiContact
                {
                    Name = "Lucas Inocencio Pires",
                    Email = "piresilucas@gmail.com",
                    Url = new Uri("https://www.github.com/LucasInoceencio")
                },
                License = new OpenApiLicense
                {
                    Name = "MIT",
                    Url = new Uri("https://www.github.com/LucasInoceencio/Escola")
                }
            });
        });
    }

    public static void UseSwaggerSetup(this IApplicationBuilder app)
    {
        if (app == null) throw new ArgumentNullException(nameof(app));

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        });
    }
}