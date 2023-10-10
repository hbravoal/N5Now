namespace N5.User.Domain.Interfaces.Persistence;

public interface IUnitOfWork : IDisposable
{
    IUserPermissionRepository UserPermissionRepository { get; }

    void Rollback();

    void Save();

    Task SaveAsync(CancellationToken cancellationToken = default);
}