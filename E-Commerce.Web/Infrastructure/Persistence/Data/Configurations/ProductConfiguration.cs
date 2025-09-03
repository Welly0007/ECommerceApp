using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Data.Configurations
{
	public class ProductConfiguration : IEntityTypeConfiguration<Product>
	{
		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.HasOne(p => p.ProductType)
				.WithMany()
				.HasForeignKey(p => p.TypeId);
			builder.HasOne(p => p.ProductBrand)
				.WithMany()
				.HasForeignKey(p => p.BrandId);
		}
	}
}
