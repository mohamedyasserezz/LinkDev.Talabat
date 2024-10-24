namespace LinkDev.Talabat.Infrastructure.Persistence.Data.Config.Products;
internal class CategoryConfigurations : BaseEntityConfigurations<ProductCategory, int>
{
    public override void Configure(EntityTypeBuilder<ProductCategory> builder)
    {
        base.Configure(builder);
        builder.Property(C => C.Name).IsRequired();
    }
}

