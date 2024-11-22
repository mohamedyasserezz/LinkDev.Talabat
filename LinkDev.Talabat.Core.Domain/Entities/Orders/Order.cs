using System.ComponentModel.DataAnnotations.Schema;

namespace LinkDev.Talabat.Core.Domain.Entities.Orders
{
    public class Order : BaseAuditableEntity<int>
    {
        public required string BuyerEmail { get; set; }
        public DateTime dateDate { get; set; } = DateTime.UtcNow;
        public OrderStatus Status { get; set; }
        public required Address ShippingAddress { get; set; }
        public int? DelivertMethodId { get; set; }
        public virtual DeliveryMethod? DeliveryMethod { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; } = new HashSet<OrderItem>();

        public decimal Subtotal { get; set; }

        public decimal GetTotal () => Subtotal + DeliveryMethod!.Cost;
        public string PaymentIntentId { get; set; } = "";
    }
}
