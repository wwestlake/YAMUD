using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LagDaemon.YAMUD.API.Services;

    public interface IUnitOfWork : IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class;
        int SaveChanges();
        Task<int> SaveChangesAsync();
        EntityEntry Entry<T>(T entity) where T : class;
    }
