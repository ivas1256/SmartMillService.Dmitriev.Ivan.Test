using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace SmartMillService.Dmitriev.Ivan.Test.ConsoleApp;

internal class Program
{
    static async Task Main(string[] args)
    {
        var builder = Host.CreateApplicationBuilder(args);

        var configuration = builder.Configuration;

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Console()
            .WriteTo.File("test-sms-console-app-.log", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        builder.Logging.ClearProviders();
        builder.Logging.AddSerilog();

        builder.Services.AddDbContext<DbContext>(options => options.UseNpgsql(configuration.GetConnectionString("DbConnectionString")));

        builder.Services.RegisterGrpc(configuration);

        builder.Services.AddScoped<MainJob>();

        var host = builder.Build();

        using (var scope = host.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<DbContext>();
            await db.Database.MigrateAsync();
        }

        var mainJob = host.Services.GetRequiredService<MainJob>();
        await mainJob.Run();
    }
}
