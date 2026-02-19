namespace SmartMillService.Dmitriev.Ivan.Test.ConsoleApp.Models
{
    public class MenuItemBarcode
    {
        public Guid Id { get; set; }
        public Guid MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; }
        public string Barcode { get; set; }
    }
}
