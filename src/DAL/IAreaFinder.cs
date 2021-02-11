using System.Threading.Tasks;

namespace WeatherService.DAL
{
    public interface IAreaFinder
    {
        Task<string?> GetByCoordinates(int latitude, int longitude);
    }
}