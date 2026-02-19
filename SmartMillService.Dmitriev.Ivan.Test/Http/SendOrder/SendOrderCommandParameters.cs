using SmartMillService.Dmitriev.Ivan.Test.Model;

namespace SmartMillService.Dmitriev.Ivan.Test.Http.SendOrder
{
    public class SendOrderCommandParameters
    {
        public Guid OrderId { get; set; }
        public List<OrderItem> MenuItems { get; set; } = [];
    }
}
