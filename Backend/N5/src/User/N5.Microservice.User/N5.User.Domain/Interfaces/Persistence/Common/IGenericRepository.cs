using N5.User.Domain.Interfaces.Entities;

namespace N5.User.Domain.Interfaces.Persistence.Common;

public interface IGenericRepository<TEntity, TId> : IRepositoryBase<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : struct
{
}