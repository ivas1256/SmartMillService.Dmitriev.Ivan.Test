namespace SmartMillService.Dmitriev.Ivan.Test.ConsoleApp.Models
{
    public class Order
    {
        public Guid Id { get; set; }   
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();
    }
}
