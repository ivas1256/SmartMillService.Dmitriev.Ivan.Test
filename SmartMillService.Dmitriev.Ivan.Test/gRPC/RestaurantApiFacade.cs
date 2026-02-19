using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using SmartMillService.Dmitriev.Ivan.Test.Model;
using Sms.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMillService.Dmitriev.Ivan.Test.gRPC
{

    internal class RestaurantApiFacade : IRestaurantApiFacade
    {
        private readonly SmsTestService.SmsTestServiceClient _client;
        private readonly gRPCApiConfig _config;

        public RestaurantApiFacade(IOptions<gRPCApiConfig> config)
        {
            _config = config.Value;

            var ch = GrpcChannel.ForAddress(_config.Address);
            _client = new SmsTestService.SmsTestServiceClient(ch);
        }

        public async Task<Model.GetMenuResponse> GetMenu(bool withPrice)
        {
            var resp = await _client.GetMenuAsync(new Google.Protobuf.WellKnownTypes.BoolValue { Value = withPrice });

            return new Model.GetMenuResponse
            {
                Success = resp.Success,
                ErrorMessage = resp.ErrorMessage,
                Data = resp.MenuItems.Select(x => new Model.MenuItem
                {
                    Name = x.Name,
                    Article = x.Article,
                    Barcodes = x.Barcodes.ToList(),
                    FullPath = x.FullPath,
                    Id = x.Id,
                    IsWeighted = x.IsWeighted,
                    Price = (decimal)x.Price
                }).ToList()
            };
        }

        public async Task<Model.SendOrderResponse> SendOrder(Model.Order order)
        {
            var grpcOrder = new Sms.Test.Order
            {
                Id = order.Id.ToString()
            };
            grpcOrder.OrderItems.AddRange(order.OrderItems.Select(x => new Sms.Test.OrderItem
            {
                Id = x.Id,
                Quantity = x.Quantity
            }));

            var resp = await _client.SendOrderAsync(grpcOrder);

            return new Model.SendOrderResponse
            {
                ErrorMessage = resp.ErrorMessage,
                Success = resp.Success,
            };
        }
    }
}
