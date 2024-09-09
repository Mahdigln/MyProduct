using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Identity;
public class RoleEntityTypeConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.HasData(
            new Role { Id = 1, ConcurrencyStamp = "4776a1b2-dbe4-4056-82ec-8bed211d1454", Name = "User", NormalizedName = "USER" }
            );
        builder.ToTable("Roles");
    }
}
