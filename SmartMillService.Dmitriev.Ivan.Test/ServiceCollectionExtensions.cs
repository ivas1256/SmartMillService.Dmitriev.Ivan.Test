using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Options;
using SmartMillService.Dmitriev.Ivan.Test.gRPC;
using SmartMillService.Dmitriev.Ivan.Test.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SmartMillService.Dmitriev.Ivan.Test
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterHttp(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<HttpApiConfig>(configuration.GetSection(nameof(HttpApiConfig)));

            services.AddHttpClient<Http.RestaurantApiFacade>((sp, client) =>
            {
                var config = sp.GetRequiredService<IOptions<HttpApiConfig>>().Value;

                client.BaseAddress = new Uri( config.BaseAddress);

                var credentials = Convert.ToBase64String(Encoding.Default.GetBytes($"{config.Login}:{config.Password}"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
            });

            services.AddScoped<IRestaurantApiFacade, Http.RestaurantApiFacade>();

            return services;
        }

        public static IServiceCollection RegisterGrpc(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<gRPCApiConfig>(configuration.GetSection(nameof(gRPCApiConfig)));

            services.AddScoped<IRestaurantApiFacade, gRPC.RestaurantApiFacade>();

            return services;
        }
    }
}
