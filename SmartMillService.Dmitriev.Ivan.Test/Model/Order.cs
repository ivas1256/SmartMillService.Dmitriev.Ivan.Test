namespace SmartMillService.Dmitriev.Ivan.Test.Model
{
    public class Order
    {
        public Guid Id { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
