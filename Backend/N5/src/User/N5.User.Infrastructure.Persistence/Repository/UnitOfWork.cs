using N5.User.Domain.Interfaces.Persistence;
using N5.User.Infrastructure.Persistence.Context;

namespace N5.User.Infrastructure.Persistence.Repository;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private bool disposedValue;
    private readonly UserContext _context;

    /// <summary>
    /// User P repository
    /// </summary>
    private IUserPermissionRepository? _userPermissionRepository;

    public IUserPermissionRepository UserPermissionRepository => _userPermissionRepository ??= new UserPermissionRepository(_context);


    public UnitOfWork(UserContext context)
        => _context = context;

    /// <summary>
    /// rollbak process
    /// </summary>
    public void Rollback() => _context.Dispose();

    /// <summary>
    /// save all process in context
    /// </summary>
    public void Save() => _context.SaveChanges();

    /// <summary>
    /// save all process async in context
    /// </summary>
    public async Task SaveAsync(CancellationToken cancellationToken = default) => await _context.SaveChangesAsync(cancellationToken);

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
        }
    }

    // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    // ~UnitOfWork()
    // {
    //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
    //     Dispose(disposing: false);
    // }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}