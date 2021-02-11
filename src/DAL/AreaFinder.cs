
using System;
using System.Threading.Tasks;

namespace WeatherService.DAL
{
    public class AreaFinder : IAreaFinder
    {
        public Task<string?> GetByCoordinates(int latitude, int longitude)
        {
            throw new NotImplementedException();
        }
    }
}