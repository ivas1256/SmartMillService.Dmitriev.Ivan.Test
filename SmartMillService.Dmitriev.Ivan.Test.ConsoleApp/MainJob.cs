using SmartMillService.Dmitriev.Ivan.Test.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartMillService.Dmitriev.Ivan.Test.ConsoleApp
{
    public record OrderInputItem
    {
        public string Code { get; set; }
        public double Quantity { get; set; }
    }

    internal class MainJob : IDisposable
    {
        private readonly IRestaurantApiFacade _restaurantApiFacade;
        private readonly DbContext _dbContext;

        public MainJob(IRestaurantApiFacade restaurantApiFacade, DbContext dbContext)
        {
            _restaurantApiFacade = restaurantApiFacade;
            _dbContext = dbContext;
        }

        public async Task Run()
        {
            if (!await LoadAndDisplayMenu())
                return;

            while (true)
            {
                var orderItemsInput = Console.ReadLine()
                    .Split(';')
                    .Select(x =>
                    {
                        var t = x.Split(':');
                        var code = t[0];
                        var quantity = double.Parse(t[1]);

                        return new OrderInputItem
                        {
                            Code = code,
                            Quantity = quantity
                        };
                    });

                if (orderItemsInput.Any(x => x.Quantity <= 0))
                {
                    Console.WriteLine("Количество для позиций должно быть больше нуля");
                    continue;
                }

                var allCodes = orderItemsInput.Select(x => x.Code).Distinct().ToList();
                var menuItems = _dbContext.MenuItems
                    .Where(x => allCodes.Contains(x.Article))
                    .ToList();

                if (menuItems.Select(x => x.Article).Distinct().Count() != allCodes.Count)
                {
                    Console.WriteLine("Один из введеных кодов не существует");
                    continue;
                }

                var order = new Model.Order
                {
                    Id = Guid.NewGuid(),
                    OrderItems = menuItems.Select(x => new Model.OrderItem
                    {
                        Id = x.Id.ToString(),
                        Quantity = orderItemsInput.First(_ => _.Code == x.Article).Quantity
                    }).ToList()
                };
                await SendOrder(order);

                break;
            }
        }

        private async Task SendOrder(Model.Order order)
        {
            try
            {
                var resp = await _restaurantApiFacade.SendOrder(order);

                if (resp.Success)
                {
                    Console.WriteLine("УСПЕШНО");
                }
                else
                {
                    Console.WriteLine(resp.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private async Task<bool> LoadAndDisplayMenu()
        {
            try
            {
                var menu = await _restaurantApiFacade.GetMenu(true);
                if (menu.Success is false)
                {
                    Console.WriteLine(menu.ErrorMessage);
                    return false;
                }


                _dbContext.MenuItems.AddRange(menu.Data.Select(x => new Models.MenuItem
                {
                    Id = new Guid(x.Id),
                    Article = x.Article,
                    FullPath = x.FullPath,
                    IsWeighted = x.IsWeighted,
                    Name = x.Name,
                    Price = x.Price,
                    Barcodes = x.Barcodes.Select(_ => new Models.MenuItemBarcode
                    {
                        Barcode = _
                    }).ToList()
                }));

                await _dbContext.SaveChangesAsync();

                foreach (var menuItem in menu.Data)
                {
                    Console.WriteLine($"{menuItem.Name}-{menuItem.Article}-{menuItem.Price}");
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка получения меню");
                return false;
            }
        }

        public void Dispose()
        {
            _dbContext?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
