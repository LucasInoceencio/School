using Microsoft.EntityFrameworkCore;
using School.Api.Configurations;
using School.Indentity.Data;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
LogsConfig.ConfigureLogs();
builder.Host.UseSerilog();
builder.Services.AddDatabaseConfiguration(builder.Configuration);
builder.Services.AddDependencyInjectionConfiguration();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();
builder.Services.AddAutoMapperConfiguration();
builder.Services.AddValidatorConfiguration();
builder.Services.AddFluentMigrator(builder.Configuration);
builder.Services.AddAuthentication(builder.Configuration);
builder.Services.AddCors();
builder.Services.AddAuthorizationPolicies();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors(c =>
    c.SetIsOriginAllowed(orign => true)
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<IdentityDataContext>();
    context.Database.Migrate();
}

app.UpdateDatabase();

app.Run();
