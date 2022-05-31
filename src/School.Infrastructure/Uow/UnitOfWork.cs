using School.Domain.Interfaces;
using School.Infrastructure.Context;

namespace School.Infrastructure.Uow;

public class UnitOfWork : IUnitOfWork
{
    private readonly SchoolContext _context;

    public UnitOfWork(SchoolContext context)
    {
        _context = context;
    }

    public async Task<bool> Commit()
    {
        var success = (await _context.SaveChangesAsync()) > 0;

        // Possibility to dispatch domain events, etc
        return success;
    }

    public void Dispose() =>
        _context.Dispose();

    public Task Rollback()
    {
        // Rollback anything, if necessary
        return Task.CompletedTask;
    }
}