using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace WeatherService
{
    public class Program
    {
        public static async Task Main(params string[] args) =>
            await CreateHostBuilder(args).Build().RunAsync(CancellationToken);

        public static CancellationToken CancellationToken { get; set; }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, configBuilder) =>
                    configBuilder.AddJsonFile("appsettings.json", false, false))
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    }
}