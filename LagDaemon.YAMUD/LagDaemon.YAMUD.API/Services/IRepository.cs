using LagDaemon.YAMUD.API.Services;
using System.Linq.Expressions;

namespace LagDaemon.YAMUD.API;

public interface IRepository<T> where T : class
{
    IEnumerable<T> GetAll();
    IEnumerable<T> Get(Expression<Func<T, bool>> filter = null,
                       Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                       string includeProperties = "");

    Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter = null,
                   Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                   string includeProperties = "");


    IEnumerable<T> Get(IQuerySpec<T> querySpec);
    Task<IEnumerable<T>> GetAsync(IQuerySpec<T> querySpec);
    T GetSingle(Expression<Func<T, bool>> filter);
    T GetById(Guid id);
    void Insert(T entity);
    void Update(T entity);
    void Delete(Guid id);
    void Delete(T entity);
    long Count();
    Task<long> CountAsync();
}


