namespace SmartMillService.Dmitriev.Ivan.Test.ConsoleApp.Models
{
    public class OrderItem
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }
        public Order Order { get; set; }

        public Guid MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; }

        public decimal Quantity { get; set; }
    }
}
