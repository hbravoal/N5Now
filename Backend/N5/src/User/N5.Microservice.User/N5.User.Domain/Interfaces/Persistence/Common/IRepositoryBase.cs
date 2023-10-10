using Ardalis.Specification;
using N5.User.Domain.Entities;

namespace N5.User.Domain.Interfaces.Persistence.Common;

/// <summary>
/// <para>
/// A <see cref="IRepositoryBase{TEntity}" /> can be used to query and save instances of <typeparamref name="TEntity" />.
/// An <see cref="ISpecification{TEntity}"/> (or derived) is used to encapsulate the LINQ queries against the database.
/// </para>
/// </summary>
/// <typeparam name="TEntity">The type of entity being operated on by this repository.</typeparam>
public interface IRepositoryBase<TEntity, TId> : IReadRepositoryBase<TEntity, TId>
    where TEntity : class, Entities.IEntity<TId>
    where TId : struct
{
    /// <summary>
    /// Adds an entity in the database.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the <typeparamref name="TEntity" />.
    /// </returns>
    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Adds the given entities in the database
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains the <typeparamref name="IEnumerable<TEntity>" />.
    /// </returns>
    Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an entity in the database
    /// </summary>
    /// <param name="entity">The entity to update.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// update specific props by id async
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="props"></param>
    Task UpdateAsync(TId id, CancellationToken cancellationToken = default, params EntityProperty[] props);

    /// <summary>
    /// delete record by id async
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    Task DisableAsync(TId id, int userId, CancellationToken cancellationToken = default);
}