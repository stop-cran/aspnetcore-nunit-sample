using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Apps72.Dev.Data.DbMocker;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Shouldly;
using WeatherService.Controllers;

namespace WeatherService.Tests
{
    public class WeatherForecastControllerTests
    {
        private MockDbConnection _dbConnection;
        private IHost _host;
        private CancellationTokenSource _cancel;

        [SetUp]
        public void Setup()
        {
            _cancel = new CancellationTokenSource();
            _dbConnection = new MockDbConnection();

            _host = Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                    webBuilder.UseStartup<Startup>())
                .ConfigureServices(services => services
                    .AddLogging(logging => logging.ClearProviders().AddConsole())
                    .AddTransient<DbConnection>(_ => _dbConnection)
                    .AddTransient<WeatherForecastController>())
                .Build();
        }

        [Test]
        [TestCase("some area", 17)]
        public async Task ShouldGetWeather(string area, int temperatureC)
        {
            // Given
            _dbConnection.Mocks
                .When(cmd =>
                    cmd.CommandText == "dbo.GetWeather" &&
                    cmd.Parameters.Count() == 1 &&
                    cmd.Parameters.First().DbType == DbType.String)
                .ReturnsTable(MockTable.WithColumns(
                        ("Date", typeof(DateTime)),
                        ("TemperatureC", typeof(int)),
                        ("Summary", typeof(string)))
                    .AddRow(DateTime.Now, temperatureC, "test"));


            var controller = _host.Services
                .GetService<WeatherForecastController>();

            // When
            var weather = await controller.Get(area, _cancel.Token);

            // Then
            var weatherItem = weather.ShouldHaveSingleItem();

            weatherItem.TemperatureC.ShouldBe(temperatureC);
        }

        [TearDown]
        public async Task TearDown()
        {
            _host.Dispose();
            await _dbConnection.DisposeAsync();
            _cancel.Dispose();
        }
    }
}