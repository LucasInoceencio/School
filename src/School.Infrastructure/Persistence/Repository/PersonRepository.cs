using Microsoft.EntityFrameworkCore;
using School.Domain.Entities;
using School.Domain.Interfaces;
using School.Infrastructure.Persistence.Context;

namespace School.Infrastructure.Persistence.Repository;

public class PersonRepository : IPersonRepository
{
    protected readonly SchoolContext Context;
    protected readonly DbSet<Person> DbSet;

    public PersonRepository(SchoolContext context)
    {
        Context = context;
        DbSet = Context.Set<Person>();
    }

    public void Add(Person person)
    {
        // var pessoaSerializada = JsonSerializer.Serialize(person);
        // Serilog.Log.Information(pessoaSerializada);
        DbSet.Add(person);
    }

    public async Task<IEnumerable<Person>> GetAll()
    {
        IQueryable<Person> query = Context.People;
        query = query.AsNoTracking().OrderBy(or => or.Id);
        return await query.ToListAsync();
    }

    public async Task<Person> GetById(int id)
    {
        IQueryable<Person> query = Context.People;
        return await query
            .AsNoTracking()
            .FirstAsync(fd => fd.Id == id);
    }

    public void Remove(Person person)
    {
        DbSet.Remove(person);
    }

    public void Update(Person person)
    {
        DbSet.Update(person);
    }
}