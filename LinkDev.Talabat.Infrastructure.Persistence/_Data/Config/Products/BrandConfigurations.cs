namespace LinkDev.Talabat.Infrastructure.Persistence.Data.Config.Products;
internal class BrandConfigurations : BaseEntityConfigurations<ProductBrand, int>
{
    public override void Configure(EntityTypeBuilder<ProductBrand> builder)
    {
        base.Configure(builder);

        builder.Property(B => B.Name)
            .IsRequired();
    }
}

