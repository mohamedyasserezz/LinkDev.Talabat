using LinkDev.Talabat.Core.Domain.Entities.Orders;

namespace LinkDev.Talabat.Infrastructure.Persistence._Data.Config.Orders
{
    internal class OrderConfigurations : BaseAudititbleEntityConfigurations<Order, int>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            base.Configure(builder);

            builder.OwnsOne(O => O.ShippingAddress, shippingAddress => shippingAddress.WithOwner());

            builder.Property(O => O.Status)
                .HasConversion
                (
                (orderStatus) => orderStatus.ToString(),
                (orderStatus) => (OrderStatus)Enum.Parse(typeof(OrderStatus), orderStatus)
                );

            builder.Property(O => O.Subtotal)
                .HasColumnType("decimal(8,2)");

            builder.HasOne(O => O.DeliveryMethod)
                .WithMany()
                .HasForeignKey(o => o.DeliveryMethodId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasMany(O => O.Items)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
