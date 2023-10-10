using N5.User.Domain.Entities;
using N5.User.Domain.Interfaces.Persistence.Common;

namespace N5.User.Domain.Interfaces.Persistence;

public interface IUserPermissionRepository : IGenericRepository<UserPermission, int>
{
}