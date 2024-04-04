using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LagDaemon.YAMUD.API.Services
{
    public interface IQuerySpec<T>
    {
        Expression<Func<T, bool>> Filter { get; }
        Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy { get; }
        string IncludeProperties { get; }
    }
}
