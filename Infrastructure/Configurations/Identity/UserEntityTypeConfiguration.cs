using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Identity;
public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);


        builder.Property(p => p.FirstName).HasMaxLength(25);
        builder.Property(p => p.LastName).HasMaxLength(25);

        builder.ToTable("Users");
    }
}
