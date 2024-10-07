﻿namespace LinkDev.Talabat.Infrastructure.Persistence.Data.Config.Products;

internal class ProductConfigurations : BaseEntityConfigurations<Product, int>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);

        builder.Property(P => P.Name)
            .IsRequired()
            .HasMaxLength(100);



        builder.Property(P => P.Description)
            .IsRequired();

        builder.Property(P => P.Price)
            .HasColumnType("decimal(9 ,2)");

        builder.HasOne(B => B.Brand)
            .WithMany()
            .HasForeignKey(B => B.BrandId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(P => P.Category)
            .WithMany()
            .HasForeignKey(P => P.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}

