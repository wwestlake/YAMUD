using LagDaemon.YAMUD.Data.Repositories;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace LagDaemon.YAMUD.API.Services;

public class UnitOfWork : IUnitOfWork
{
    private readonly YamudDbContext _context;
    private Dictionary<Type, object> _repositories;

    public UnitOfWork(YamudDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _repositories = new Dictionary<Type, object>();
        
    }

    public EntityEntry Entry<T>(T entity) where T : class
    {
        return _context.Entry(entity);
    }

    public IRepository<T> GetRepository<T>() where T : class
    {
        if (_repositories.ContainsKey(typeof(T)))
        {
            return _repositories[typeof(T)] as IRepository<T>;
        }

        var repository = new Repository<T>(_context);
        _repositories.Add(typeof(T), repository);
        return repository;
    }

    public int SaveChanges()
    {
        return _context.SaveChanges();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
