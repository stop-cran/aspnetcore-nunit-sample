using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Dapper;

namespace WeatherService.DAL
{
    public class WeatherRepository : IWeatherRepository
    {
        private readonly DbConnection _dbConnection;

        public WeatherRepository(DbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }


        public async Task<IEnumerable<WeatherDto>> GetWeather(string area, CancellationToken cancellationToken) =>
            await _dbConnection.QueryAsync<WeatherDto>(
                new CommandDefinition("dbo.GetWeather",
                    new { area },
                    commandType: CommandType.StoredProcedure,
                    cancellationToken: cancellationToken));
    }
}