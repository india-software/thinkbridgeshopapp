using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThinkBridge.Shop.Core.Domain.Catalog;

namespace ThinkBridge.Shop.Data.Mapping.Catalog
{
    /// <summary>
    /// Represents a product category mapping configuration
    /// </summary>
    public partial class ProductCategoryMap : ThinkBridgeEntityTypeConfiguration<ProductCategory>
    {
        #region Methods

        /// <summary>
        /// Configures the entity
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity</param>
        public override void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable(nameof(ProductCategory));
            builder.HasKey(productCategory => productCategory.Id);

            builder.HasOne(productCategory => productCategory.Category)
                .WithMany()
                .HasForeignKey(productCategory => productCategory.CategoryId)
                .IsRequired();

            builder.HasOne(productCategory => productCategory.Product)
                .WithMany(product => product.ProductCategories)
                .HasForeignKey(productCategory => productCategory.ProductId)
                .IsRequired();

            base.Configure(builder);
        }

        #endregion
    }
}