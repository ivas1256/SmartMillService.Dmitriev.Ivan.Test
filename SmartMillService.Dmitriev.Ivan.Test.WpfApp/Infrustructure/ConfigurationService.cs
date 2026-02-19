using Microsoft.Extensions.Configuration;

namespace SmartMillService.Dmitriev.Ivan.Test.WpfApp
{
    public class ConfigurationService
    {
        public IConfigurationRoot Configuration { get; }

        public ConfigurationService()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
                .Build();
        }

        public List<string> GetVariableNames() => Configuration.GetSection("EnvironmentVariables")
            .GetChildren()
            .Select(x => x.ToString())
            .ToList();
    }
}
