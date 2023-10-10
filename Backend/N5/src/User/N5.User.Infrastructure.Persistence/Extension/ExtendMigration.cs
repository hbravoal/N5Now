using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
using N5.User.Infrastructure.Persistence.Context;
using System.Diagnostics.CodeAnalysis;

namespace N5.User.Infrastructure.Persistence.Extension;

/// <summary>
/// Extension execute migration
/// </summary>
/// <remarks>Exclude to coverage because memory database not use migration</remarks>
[ExcludeFromCodeCoverage]
public static class ExtendMigration
{
    /// <summary>
    /// Execute Migration
    /// </summary>
    /// <param name="host">host instance</param>
    /// <example>host.ExecuteMigration(args);</example>
    /// <remarks>remember use attribute --run-migration when execute your application</remarks>
	//public static IHost ExecuteMigration(this IHost host, string[] args)
 //   {
 //       if (args.Contains("--run-migration"))
 //       {
 //           var context = host.Services.GetService<UserContext>();
 //           ArgumentNullException.ThrowIfNull(context);
 //           if (context.Database.GetPendingMigrations().Any())
 //               context.Database.Migrate();
 //       }

 //       return host;
 //   }
}