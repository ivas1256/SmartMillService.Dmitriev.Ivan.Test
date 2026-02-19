using SmartMillService.Dmitriev.Ivan.Test.Model;

namespace SmartMillService.Dmitriev.Ivan.Test
{
    public interface IRestaurantApiFacade
    {
        Task<GetMenuResponse> GetMenu(bool withPrice);
        Task<SendOrderResponse> SendOrder(Order order);
    }
}