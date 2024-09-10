using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Product;

public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Entities.Product.Product>
{
	public void Configure(EntityTypeBuilder<Domain.Entities.Product.Product> builder)
	{
		ArgumentNullException.ThrowIfNull(builder);


		builder.ToTable("Products");

		builder.Property(p => p.Name)
			.IsRequired()
			.HasMaxLength(100);

		builder.Property(p => p.ManufactureEmail)
			.IsRequired()
			.HasMaxLength(100);

		builder.Property(p => p.ManufacturePhone)
			.HasMaxLength(11);

		builder.Property(p => p.ProduceDate)
			.IsRequired();

		builder.HasIndex(p => new { p.ManufactureEmail, p.ProduceDate })
			.HasDatabaseName("IX_ManufactureEmail_ProduceDate")
			.IsUnique(true);

		builder.HasQueryFilter(p => !p.IsDeleted);

		builder.HasOne(p => p.User)
			.WithMany(u => u.Products)
			.HasForeignKey(p => p.UserId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}