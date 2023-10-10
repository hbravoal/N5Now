using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using N5.User.Domain.Interfaces.Entities;
using N5.User.Domain.Interfaces.Persistence.Common;
using N5.User.Domain.Interfaces.Entities;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using N5.User.Domain.Entities;
using N5.User.Infrastructure.Persistence.Repository.Common;
using N5.User.Infrastructure.Persistence.Utils;

namespace N5.User.Infrastructure.Persistence.Repository.Common;

/// <summary>
/// generic repository extend from Ardalis.Specification RepositoryBase
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public abstract class GenericRepository<TEntity, TId> : IGenericRepository<TEntity, TId>
    where TEntity : class, Domain.Interfaces.Entities.IEntity<TId>
    where TId : struct
{
    private readonly ISpecificationEvaluator _specificationEvaluator;
    internal readonly DbContext _context;

    public GenericRepository(DbContext context)
        : this(context, SpecificationEvaluator.Default) { }

    /// <inheritdoc/>
    public GenericRepository(DbContext context, ISpecificationEvaluator specificationEvaluator)
        => (_context, _specificationEvaluator) = (context, specificationEvaluator);

    #region CUD

    /// <inheritdoc/>
    public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        => await Task.Factory.StartNew(() =>
        {
            _context.Set<TEntity>().Add(entity);
            return entity;
        },
        cancellationToken);

    /// <inheritdoc/>
    public virtual async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
        => await Task.Factory.StartNew(() =>
        {
            _context.Set<TEntity>().AddRange(entities);
            return entities;
        },
        cancellationToken);

    /// <inheritdoc/>
    public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        => await Task.Factory.StartNew(() => _context.Set<TEntity>().Update(entity), cancellationToken);

    /// <summary>
    /// update all entity
    /// </summary>
    /// <param name="entityToUpdate"></param>
    private void Update(TId id, params EntityProperty[] props)
    {
        var entity = Activator.CreateInstance<TEntity>();
        entity.Id = id;
        Update(entity, props);
    }

    /// <summary>
    /// update all entity
    /// </summary>
    /// <param name="entityToUpdate"></param>
    private void Update(TEntity? entity, params EntityProperty[] props)
    {
        if (entity is not null)
        {
            var _props = props.ToList();
            _props.Add(new EntityProperty("LastUpdate", DateTime.UtcNow.ToString()));

            var properties = typeof(TEntity).GetProperties().Where(
                x => _props.Any(p => p?.Name?.ToLower() == x.Name?.ToLower())
            );

            foreach (var prop in properties)
            {
                var propValue = _props.FirstOrDefault(x => x?.Name?.ToLower() == prop.Name?.ToLower())?.Value?.ToString()?.Convert(prop.PropertyType);

                if (propValue != null)
                    prop.SetValue(entity, propValue);
            }

            _context.Set<TEntity>().Attach(entity);

            foreach (var prop in properties)
            {
                if (!prop.PropertyType.IsImplementationOfGenericType(typeof(IEntity)) && (prop.PropertyType == typeof(string) || !prop.PropertyType.IsImplementationOfGenericType(typeof(IEnumerable<>))))
                    _context.Entry(entity).Property(prop.Name).IsModified = true;
            }
        }
    }

    /// <inheritdoc/>
    public virtual Task UpdateAsync(TId id, CancellationToken cancellationToken = default, params EntityProperty[] props)
        => !Equals(id, id.GetDefault()) ? Task.Factory.StartNew(() => Update(id, props), cancellationToken) : throw new ArgumentException();

    /// <inheritdoc/>
    public virtual Task DisableAsync(TId id, int userId, CancellationToken cancellationToken = default)
        => UpdateAsync(
            id,
            cancellationToken,
            new EntityProperty("Enabled", "false"),
            new EntityProperty("DeleteUserId", userId.ToString())
        );

    #endregion CUD

    #region Read

    /// <inheritdoc/>
    public virtual Task<List<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        CancellationToken cancellationToken = default,
        bool withDisabled = false,
        string includeProperties = ""
    )
        => _context.BuildQuery(withDisabled, filter, orderBy, includeProperties)
                   .ToListAsync(cancellationToken);

    /// <inheritdoc/>
    public virtual Task<List<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        CancellationToken cancellationToken = default,
        bool withDisabled = false,
        params Expression<Func<TEntity, dynamic>>[] navigationPropertyPaths
    )
        => _context.BuildQuery(withDisabled, filter, orderBy, navigationPropertyPaths: navigationPropertyPaths)
                   .ToListAsync(cancellationToken);

    /// <inheritdoc/>
    public virtual Task<List<TEntity>> GetPaginingAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        CancellationToken cancellationToken = default,
        int page = 1,
        int size = 100,
        bool withDisabled = false,
        params Expression<Func<TEntity, dynamic>>[] navigationPropertyPaths
    )
        => _context.BuildQuery(withDisabled, filter, orderBy, navigationPropertyPaths: navigationPropertyPaths)
                   .Skip((page - 1) * size).Take(size)
                   .ToListAsync(cancellationToken);

    /// <inheritdoc/>
    [ExcludeFromCodeCoverage]
    public virtual Task<List<TResult>> GetAsync<TResult>(
    Expression<Func<TEntity, TResult>> selector,
    Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        bool withDisabled = false,
        CancellationToken cancellationToken = default
    )
        => _context.BuildQuery(withDisabled, filter, orderBy)
                   .Select(selector)
                   .ToListAsync(cancellationToken);

    /// <inheritdoc/>
    public virtual Task<TEntity?> GetFirstOrDefaultAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        CancellationToken cancellationToken = default,
        bool withDisabled = false,
        string includeProperties = ""
    )
        => _context.BuildQuery(withDisabled, filter, orderBy, includeProperties)
                   .FirstOrDefaultAsync(cancellationToken);

    /// <inheritdoc/>
    public virtual Task<TEntity?> GetFirstOrDefaultAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        CancellationToken cancellationToken = default,
        bool withDisabled = false,
        params Expression<Func<TEntity, dynamic>>[] navigationPropertyPaths
    )
        => _context.BuildQuery(withDisabled, filter, orderBy, navigationPropertyPaths: navigationPropertyPaths)
                   .FirstOrDefaultAsync(cancellationToken);

    /// <inheritdoc/>
    [ExcludeFromCodeCoverage]
    public virtual Task<TResult?> GetFirstOrDefaultAsync<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        bool withDisabled = false,
        CancellationToken cancellationToken = default
    )
        => _context.BuildQuery(withDisabled, filter, orderBy)
                   .Select(selector)
                   .FirstOrDefaultAsync(cancellationToken);

    /// <inheritdoc/>
    [ExcludeFromCodeCoverage]
    public virtual Task<TEntity?> GetSingleOrDefaultAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        CancellationToken cancellationToken = default,
        bool withDisabled = false,
        string includeProperties = "",
        bool disabledTracking = true
    )
        => _context.BuildQuery(withDisabled, filter, includeProperties: includeProperties, disabledTracking: disabledTracking)
                   .SingleOrDefaultAsync(cancellationToken);

    /// <inheritdoc/>
    [ExcludeFromCodeCoverage]
    public virtual Task<TEntity?> GetSingleOrDefaultAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        CancellationToken cancellationToken = default,
        bool withDisabled = false,
        bool disabledTracking = true,
        params Expression<Func<TEntity, dynamic>>[] navigationPropertyPaths
    )
        => _context.BuildQuery(withDisabled, filter, navigationPropertyPaths: navigationPropertyPaths, disabledTracking: disabledTracking)
                   .SingleOrDefaultAsync(cancellationToken);

    /// <inheritdoc/>
    public virtual Task<TResult?> GetSingleOrDefaultAsync<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? filter = null,
        bool withDisabled = false,
        bool disabledTracking = true,
        CancellationToken cancellationToken = default
    )
        => _context.BuildQuery(withDisabled, filter, disabledTracking: disabledTracking)
                   .Select(selector)
                   .SingleOrDefaultAsync(cancellationToken);

    /// <inheritdoc/>
    public virtual async Task<TEntity?> GetByIdAsync(TId id, bool withDisabled = false, CancellationToken cancellationToken = default)
        => await _context.NewQuery<TEntity>(withDisabled).FirstOrDefaultAsync(x => (object)x.Id == (object)id, cancellationToken: cancellationToken);

    /// <inheritdoc/>
    public virtual async Task<List<TEntity>> ListAsync(bool withDisabled = false, CancellationToken cancellationToken = default)
        => await _context.NewQuery<TEntity>(withDisabled).ToListAsync(cancellationToken);

    /// <inheritdoc/>
    public virtual async Task<int> CountAsync(bool withDisabled = false, CancellationToken cancellationToken = default)
        => await _context.NewQuery<TEntity>(withDisabled).CountAsync(cancellationToken);

    /// <inheritdoc/>
    public virtual async Task<bool> AnyAsync(bool withDisabled = false, CancellationToken cancellationToken = default)
        => await _context.NewQuery<TEntity>(withDisabled).AnyAsync(cancellationToken);

    #endregion Read
}