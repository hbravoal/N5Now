using Microsoft.EntityFrameworkCore;
using N5.User.Domain.Entities;
using N5.User.Infrastructure.Persistence.EntityConfigurations;

namespace N5.User.Infrastructure.Persistence.Context;

/// <summary>
/// Realty Context
/// </summary>
public class UserContext : DbContext
{
    public const string DEFAULT_SCHEMA = "User";

    /// <summary>
    /// Constructor
    /// </summary>
    public UserContext()
    {
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="options">Options Connections</param>
    public UserContext(DbContextOptions<UserContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserPermissionConfiguration());
    }

    /// <summary>
    /// Transaction
    /// </summary>
    public DbSet<UserPermission> UserPermission { get; set; }

}