using LagDaemon.YAMUD.API.Services;
using LagDaemon.YAMUD.Model.Items;
using System.Linq.Expressions;

namespace LagDaemon.YAMUD.WebAPI.Services.ItemsServices;

public class GeneralItemQuerySpec : IQuerySpec<Item>
{

    public GeneralItemQuerySpec(
        Expression<Func<Item, bool>> filter,
        Func<IQueryable<Item>, IOrderedQueryable<Item>> orderBy,
        string includeProperties)
    {
        Filter = filter;
        OrderBy = orderBy;
        IncludeProperties = includeProperties;
    }

    public Expression<Func<Item, bool>> Filter { get; }

    public Func<IQueryable<Item>, IOrderedQueryable<Item>> OrderBy { get; }

    public string IncludeProperties { get; }
}
