using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using N5.User.Domain.Interfaces.Entities;
using N5.User.Infrastructure.Persistence.Context;

namespace N5.User.Infrastructure.Persistence.EntityConfigurations;

public static class BaseEntityTypeConfiguration
{
    public static void ConfigureBase<TEntity, TId>(this EntityTypeBuilder<TEntity> builder)
        where TEntity : class, IEntity<TId>
        where TId : struct
    {
        builder.ToTable(typeof(TEntity).Name, UserContext.DEFAULT_SCHEMA);
        builder.HasKey(ci => ci.Id);
    }
}