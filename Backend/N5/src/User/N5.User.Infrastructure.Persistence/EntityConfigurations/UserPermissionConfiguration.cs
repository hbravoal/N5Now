using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using N5.User.Domain.Entities;

namespace N5.User.Infrastructure.Persistence.EntityConfigurations;


public class UserPermissionConfiguration : IEntityTypeConfiguration<UserPermission>
{
    public void Configure(EntityTypeBuilder<UserPermission> builder)
    {
        builder.ConfigureBase<UserPermission, int>();
    }
}