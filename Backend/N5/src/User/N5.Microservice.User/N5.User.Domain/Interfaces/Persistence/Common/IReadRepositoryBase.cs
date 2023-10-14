using System.Linq.Expressions;

namespace N5.User.Domain.Interfaces.Persistence.Common;

/// <summary>
/// <para>
/// A <see cref="IRepositoryBase{TEntity}" /> can be used to query instances of <typeparamref name="TEntity" />.
/// An <see cref="ISpecification{TEntity}"/> (or derived) is used to encapsulate the LINQ queries against the database.
/// </para>
/// </summary>
/// <typeparam name="TEntity">The type of entity being operated on by this repository.</typeparam>
/// <typeparam name="TId">The type of primary key.</typeparam>
public interface IReadRepositoryBase<TEntity, TId>
    where TEntity : class, Entities.IEntity<TId>
    where TId : struct
{
    /// <summary>
    /// Get all with filter, ordering and include
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="orderBy"></param>
    /// <param name="includeProperties"></param>
    /// <returns></returns>
    Task<List<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        CancellationToken cancellationToken = default,
        bool withDisabled = false,
        string includeProperties = "");

    /// <summary>
    /// Get all with filter, ordering and include
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="orderBy"></param>
    /// <param name="navigationPropertyPaths"></param>
    /// <returns></returns>
    Task<List<TEntity>> GetAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        CancellationToken cancellationToken = default,
        bool withDisabled = false,
        params Expression<Func<TEntity, dynamic>>[] navigationPropertyPaths);

    /// <summary>
    /// Get all with filter, ordering, pagining and include
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="orderBy"></param>
    /// <param name="navigationPropertyPaths"></param>
    /// <returns></returns>
    Task<List<TEntity>> GetPaginingAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        int page=1,
        int size = 100,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        CancellationToken cancellationToken = default,
        bool withDisabled = false,
        params Expression<Func<TEntity, dynamic>>[] navigationPropertyPaths);

    /// <summary>
    /// Get all with selector, filter, ordering and include
    /// </summary>
    /// /// <typeparam name="TResult">The type of selector result.</typeparam>
    /// <param name="selector"></param>
    /// <param name="filter"></param>
    /// <param name="orderBy"></param>
    /// <returns></returns>
    Task<List<TResult>> GetAsync<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        bool withDisabled = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get first or default with filter, ordering and include
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="orderBy"></param>
    /// <param name="includeProperties"></param>
    /// <returns></returns>
    Task<TEntity?> GetFirstOrDefaultAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        CancellationToken cancellationToken = default,
        bool withDisabled = false,
        string includeProperties = "");

    /// <summary>
    /// Get first or default with filter, ordering and include
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="orderBy"></param>
    /// <param name="navigationPropertyPaths"></param>
    /// <returns></returns>
    Task<TEntity?> GetFirstOrDefaultAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        CancellationToken cancellationToken = default,
        bool withDisabled = false,
        params Expression<Func<TEntity, dynamic>>[] navigationPropertyPaths);

    /// <summary>
    /// Get first or default with selector, filter, ordering and include
    /// </summary>
    /// <typeparam name="TResult">The type of selector result.</typeparam>
    /// <param name="selector"></param>
    /// <param name="filter"></param>
    /// <param name="orderBy"></param>
    /// <returns></returns>
    Task<TResult?> GetFirstOrDefaultAsync<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
        bool withDisabled = false,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Get first or default with filter, ordering and include
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="includeProperties"></param>
    /// <returns></returns>
    Task<TEntity?> GetSingleOrDefaultAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        CancellationToken cancellationToken = default,
        bool withDisabled = false,
        string includeProperties = "",
        bool disabledTracking = true);

    /// <summary>
    /// Get first or default with filter, ordering and include
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="navigationPropertyPaths"></param>
    /// <returns></returns>
    Task<TEntity?> GetSingleOrDefaultAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        CancellationToken cancellationToken = default,
        bool withDisabled = false,
        bool disabledTracking = true,
        params Expression<Func<TEntity, dynamic>>[] navigationPropertyPaths);

    /// <summary>
    /// Get first or default with selector, filter, ordering and include
    /// </summary>
    /// <typeparam name="TResult">The type of selector result.</typeparam>
    /// <param name="selector"></param>
    /// <param name="filter"></param>
    /// <returns></returns>
    Task<TResult?> GetSingleOrDefaultAsync<TResult>(
        Expression<Func<TEntity, TResult>> selector,
        Expression<Func<TEntity, bool>>? filter = null,
        bool withDisabled = false,
        bool disabledTracking = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Finds an entity with the given primary key value.
    /// </summary>
    /// <param name="id">The value of the primary key for the entity to be found.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the <typeparamref name="TEntity" />, or <see langword="null"/>.
    /// </returns>
    Task<TEntity?> GetByIdAsync(TId id, bool withDisabled = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// Finds all entities of <typeparamref name="TEntity" /> from the database.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains a <see cref="List{TEntity}" /> that contains elements from the input sequence.
    /// </returns>
    Task<List<TEntity>> ListAsync(bool withDisabled = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the total number of records.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains the
    /// number of elements in the input sequence.
    /// </returns>
    Task<int> CountAsync(bool withDisabled = false, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns a boolean whether any entity exists or not.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains true if the
    /// source sequence contains any elements; otherwise, false.
    /// </returns>
    Task<bool> AnyAsync(bool withDisabled = false, CancellationToken cancellationToken = default);
}