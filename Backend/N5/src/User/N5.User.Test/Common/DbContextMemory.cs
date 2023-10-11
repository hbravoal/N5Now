using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace N5.User.Test.Common;

/// <summary>
/// Memory DBContext to SQL SERVER
/// </summary>
[ExcludeFromCodeCoverage]
public static class DbContextMemory
{
    /// <summary>
    /// Create Options to memory access
    /// </summary>
    /// <typeparam name="T">Configuration Database</typeparam>
    /// <param name="nameDatabase">Name to database</param>
    /// <returns>DB context options</returns>
    public static DbContextOptions<T> CreateContextOptions<T>(string nameDatabase)
        where T : DbContext
    {
        var options = new DbContextOptionsBuilder<T>()
        .UseInMemoryDatabase(databaseName: nameDatabase)
        .Options;

        return options;
    }
}