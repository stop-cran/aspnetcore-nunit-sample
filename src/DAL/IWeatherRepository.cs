using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WeatherService.DAL
{
    public interface IWeatherRepository
    {
        Task<IEnumerable<WeatherDto>> GetWeather(string area, CancellationToken cancellationToken);
    }
}