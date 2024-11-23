using LinkDev.Talabat.Core.Domain.Entities.Orders;

namespace LinkDev.Talabat.Infrastructure.Persistence._Data.Config.Orders
{
    internal class OrderItemConfigurations : BaseAudititbleEntityConfigurations<OrderItem, int>
    {
        public override void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            base.Configure(builder);

            builder.OwnsOne(item => item.Product, Product => Product.WithOwner());
            builder.Property(item => item.Price)
                .HasColumnType("decimal(8,2)");
        }
    }
}
