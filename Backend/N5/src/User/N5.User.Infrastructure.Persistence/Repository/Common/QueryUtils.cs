using Microsoft.EntityFrameworkCore;
using N5.User.Domain.Interfaces.Entities;
using System.Linq.Expressions;

namespace N5.User.Infrastructure.Persistence.Repository.Common;

public static class QueryUtils
{
    public static IQueryable<TEntity> NewQuery<TEntity>(
        this DbContext context,
        bool withDisabled = false
    ) where TEntity : class, IEntity
    {
        var query = context.Set<TEntity>().AsNoTracking();

        if (!withDisabled)
            query = query.Where(x => x.Enabled);

        return query;
    }

    public static IQueryable<TEntity> Includes<TEntity>(
        this IQueryable<TEntity> query,
        string includeProperties = ""
    ) where TEntity : class, IEntity
    {
        foreach (var includeProperty in includeProperties.Split
            (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            query = query.Include(includeProperty);

        return query;
    }

    public static IQueryable<TEntity> Includes<TEntity>(
        this IQueryable<TEntity> query,
        params Expression<Func<TEntity, dynamic>>[] navigationPropertyPaths
    ) where TEntity : class, IEntity
    {
        foreach (var includeProperty in navigationPropertyPaths)
            query = query.Include(includeProperty);

        return query;
    }

    public static IQueryable<TEntity> BuildQuery<TEntity>(
        this IQueryable<TEntity> query,
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null
    ) where TEntity : class, IEntity
    {
        if (filter != null)
            query = query.Where(filter);

        if (orderBy != null)
            query = orderBy(query);
        else
            query = query.OrderBy(x => x.CreatedDate);

        return query;
    }

    public static IQueryable<TEntity> BuildQuery<TEntity>(
        this DbContext context,
        bool withDisabled = false,
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        string includeProperties = "",
        bool disabledTracking = true,
        params Expression<Func<TEntity, dynamic>>[] navigationPropertyPaths
    ) where TEntity : class, IEntity
    {
        var query = context.NewQuery<TEntity>(withDisabled)
                           .BuildQuery(filter, orderBy);

        if (disabledTracking)
            query = query.AsNoTracking();

        if (!string.IsNullOrEmpty(includeProperties))
            query = query.Includes(includeProperties);

        if (navigationPropertyPaths != null && navigationPropertyPaths.Length > 0)
            query = query.Includes(navigationPropertyPaths);

        return query;
    }
}