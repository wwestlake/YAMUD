using LagDaemon.YAMUD.API;
using LagDaemon.YAMUD.API.Services;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LagDaemon.YAMUD.Data.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly DbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(YamudDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<T>();
    }

    public IEnumerable<T> GetAll()
    {
        return _dbSet.ToList();
    }

    public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null,
                              Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                              string includeProperties = "")
    {
        IQueryable<T> query = _dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split
                                        (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        if (orderBy != null)
        {
            return orderBy(query).ToList();
        }
        else
        {
            return query.ToList();
        }
    }

    public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "")
    {
        IQueryable<T> query = _dbSet;

        if (filter != null)
        {
            query = query.Where(filter);
        }

        foreach (var includeProperty in includeProperties.Split
                                        (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
        {
            query = query.Include(includeProperty);
        }

        if (orderBy != null)
        {
            return orderBy(query).ToList();
        }
        else
        {
            return await query.ToListAsync();
        }
    }



    public T GetSingle(Expression<Func<T, bool>> filter)
    {
        return _dbSet.SingleOrDefault(filter);
    }

    public async Task<T> GetSingleAsync(Expression<Func<T, bool>> filter)
    {
        return await _dbSet.SingleOrDefaultAsync(filter);
    }


    public T GetById(Guid id)
    {
        return _dbSet.Find(id);
    }

    public void Insert(T entity)
    {
        _dbSet.Add(entity);
    }

    public void Update(T entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(Guid id)
    {
        T entityToDelete = _dbSet.Find(id);
        Delete(entityToDelete);
    }

    public void Delete(T entity)
    {
        if (_context.Entry(entity).State == EntityState.Detached)
        {
            _dbSet.Attach(entity);
        }
        _dbSet.Remove(entity);
    }

    public IEnumerable<T> Get(IQuerySpec<T> querySpec)
    {
        return Get(querySpec.Filter, querySpec.OrderBy, querySpec.IncludeProperties);
    }


    public async Task<IEnumerable<T>> GetAsync(IQuerySpec<T> querySpec)
    {
        return await GetAsync(querySpec.Filter, querySpec.OrderBy, querySpec.IncludeProperties);
    }

    public long Count()
    {
        return _dbSet.Count();
    }

    public async Task<long> CountAsync()
    {
        return await _dbSet.CountAsync();
    }



}
