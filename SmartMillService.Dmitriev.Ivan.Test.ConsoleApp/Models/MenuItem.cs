namespace SmartMillService.Dmitriev.Ivan.Test.ConsoleApp.Models
{
    public class MenuItem
    {
        public Guid Id { get; set; }              
        public string Article { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool IsWeighted { get; set; }
        public string FullPath { get; set; }

        public ICollection<MenuItemBarcode> Barcodes { get; set; } = new List<MenuItemBarcode>();
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
