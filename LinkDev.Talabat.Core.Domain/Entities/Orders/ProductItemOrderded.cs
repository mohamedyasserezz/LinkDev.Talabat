namespace LinkDev.Talabat.Core.Domain.Entities.Orders
{
    public class ProductItemOrderded
    {
        public int ProductId { get; set; }
        public required string ProductName { get; set; }
        public required string PictureUrl { get; set; }
    }
}
