using Microsoft.Extensions.Options;
using SmartMillService.Dmitriev.Ivan.Test.Http.GetMenu;
using SmartMillService.Dmitriev.Ivan.Test.Http.SendOrder;
using SmartMillService.Dmitriev.Ivan.Test.Model;
using System.Text;
using System.Text.Json;

namespace SmartMillService.Dmitriev.Ivan.Test.Http
{
    internal class RestaurantApiFacade : IRestaurantApiFacade
    {
        private readonly HttpClient _httpClient;
        private readonly HttpApiConfig _config;

        public RestaurantApiFacade(HttpClient httpClient, IOptions<HttpApiConfig> config)
        {
            _httpClient = httpClient;
            _config = config.Value;
        }

        public async Task<GetMenuResponse> GetMenu(bool withPrice)
        {
            var request = new GetMenuApiRequest
            {
                CommandParameters = new GetMenuCommandParameters
                {
                    WithPrice = withPrice
                }
            };

            var content = new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json");

            var resp = await _httpClient.PostAsync(_config.ApiUrl, content);

            resp.EnsureSuccessStatusCode();// хоть апи и всегда возвращает 200, но могут быть случаи с 403, 500

            var respStr = await resp.Content.ReadAsStringAsync();
            var respJson = JsonSerializer.Deserialize<GetMenuApiResponse>(respStr);

            return new GetMenuResponse
            {
                Success = respJson.Success,
                ErrorMessage = respJson.ErrorMessage,
                Data = respJson.Data
            };
        }

        public async Task<SendOrderResponse> SendOrder(Order order)
        {
            var request = new SendOrderApiRequest
            {
                CommandParameters = new SendOrderCommandParameters
                {
                    OrderId = order.Id,
                    MenuItems = order.OrderItems
                }
            };

            var content = new StringContent(
                JsonSerializer.Serialize(request),
                Encoding.UTF8,
                "application/json");

            var resp = await _httpClient.PostAsync(_config.ApiUrl, content);

            resp.EnsureSuccessStatusCode();// хоть апи и всегда возвращает 200, но могут быть случаи с 403, 500

            var respStr = await resp.Content.ReadAsStringAsync();
            var respJson = JsonSerializer.Deserialize<SendOrderApiResponse>(respStr);

            return new SendOrderResponse
            {
                ErrorMessage = respJson.ErrorMessage,
                Success = respJson.Success,
            };
        }
    }
}
