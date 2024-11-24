using LinkDev.Talabat.Core.Domain.Entities.Orders;

namespace LinkDev.Talabat.Core.Domain.Specifications.Orders
{
    public class OrderSpecification : BaseSpecification<Order, int>
    {
        public OrderSpecification(string buyerEmail)
            : base(order => order.BuyerEmail == buyerEmail)
        {
            AddIncludes();
            AddOrderByDesc(Order => Order.OrderDate);


        }
        public OrderSpecification(string buyerEmail, int orderId)
          : base(order => order.BuyerEmail == buyerEmail && order.Id == orderId)
        {
            AddIncludes();

        }
        private protected override void AddIncludes()
        {
            base.AddIncludes();
            Includes.Add(O => O.DeliveryMethod!);
            Includes.Add(O => O.Items);
        }
    }
}
