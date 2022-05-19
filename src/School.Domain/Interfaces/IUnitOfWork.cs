namespace School.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    Task<bool> Commit();
    Task Rollback();
}