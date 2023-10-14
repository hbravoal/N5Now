using Bogus;
using N5.User.Domain.Interfaces.Persistence;
using N5.User.Infrastructure.Persistence.Context;
using N5.User.Infrastructure.Persistence.Repository;
using N5.User.Test.Common;
using System.Diagnostics.CodeAnalysis;

namespace N5.User.Test.Extend;

[ExcludeFromCodeCoverage]
public static class UserContextExtend
{
    //private static RealtyContext? _context;

    public static UserContext Context =>
        new(DbContextMemory.CreateContextOptions<UserContext>("N5"));

    public static IUnitOfWork UnitOfWork =>
        new UnitOfWork(Context);




}