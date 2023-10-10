using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using N5.User.Domain.Interfaces.Persistence;
using N5.User.Infrastructure.Persistence.Context;
using N5.User.Infrastructure.Persistence.Repository;
using System.Diagnostics.CodeAnalysis;

namespace N5.User.Infrastructure.Persistence;

/// <summary>
/// dependency inyection
/// </summary>
[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
    /// <summary>
    /// Add Domain
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IUserPermissionRepository, UserPermissionRepository>();
        services.AddDbContext<UserContext>(
            options => options.UseSqlServer(configuration.GetConnectionString("SqlServer")),
            contextLifetime: ServiceLifetime.Transient
        );
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        return services;
    }
}