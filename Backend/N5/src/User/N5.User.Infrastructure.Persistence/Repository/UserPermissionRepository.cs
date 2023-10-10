using N5.User.Domain.Entities;
using N5.User.Domain.Interfaces.Persistence;
using N5.User.Infrastructure.Persistence.Context;
using N5.User.Infrastructure.Persistence.Repository.Common;

namespace N5.User.Infrastructure.Persistence.Repository;

/// <summary>
/// Broker repository
/// </summary>
public class UserPermissionRepository : GenericRepository<UserPermission, int>, IUserPermissionRepository
{
    public UserPermissionRepository(UserContext context) : base(context)
    {
    }
}