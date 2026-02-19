using Serilog;
using System.IO;

namespace SmartMillService.Dmitriev.Ivan.Test.WpfApp
{
    public static class LoggingService
    {
        public static void Configure()
        {
            Directory.CreateDirectory("logs");

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File(
                    path: "test-sms-wpf-app-.log",
                    rollingInterval: RollingInterval.Day,
                    outputTemplate:
                    "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}")
                .CreateLogger();
        }
    }
}
