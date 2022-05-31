using System.Reflection;
using Microsoft.EntityFrameworkCore;
using School.Domain.Entities;
using School.Domain.Entities.Core;

namespace School.Infrastructure.Context;

public class SchoolContext : DbContext
{
    public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) { }
    public DbSet<Person> People { get; set; }
    // public DbSet<Student> Students { get; set; }
    // public DbSet<StudentCourse> StudentsCourses { get; set; }
    // public DbSet<StudentDiscipline> StudentsDisciplines { get; set; }
    // public DbSet<Course> Courses { get; set; }
    // public DbSet<Discipline> Disciplines { get; set; }
    // public DbSet<Teacher> Teachers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        // Verificar uma forma de só criar o esquema caso não exista
        // pois no SQL Server dá erro caso já tenha o schema
        // Definindo o schema do ExemploContext
        //builder.HasDefaultSchema("public");

        // Configurando o mapeamento de todas a classes que
        // implementam IEntityTypeConfiguration de forma automática
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override int SaveChanges()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is Entity &&
                (e.State == EntityState.Added
                || e.State == EntityState.Modified));

        foreach (var entityEntry in entries)
        {
            ((Entity)entityEntry.Entity).DateHourChange = DateTime.UtcNow;

            if (entityEntry.State == EntityState.Added)
            {
                ((Entity)entityEntry.Entity).DateHourRegister = DateTime.UtcNow;
            }
        }

        return base.SaveChanges();
    }
}