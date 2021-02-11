using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherService.DAL;

namespace WeatherService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherRepository _weatherRepository;

        public WeatherForecastController(
            IWeatherRepository weatherRepository)
        {
            _weatherRepository = weatherRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherDto>> Get(
            string area,
            CancellationToken cancellationToken) =>
            await _weatherRepository.GetWeather(area, cancellationToken);
    }
}