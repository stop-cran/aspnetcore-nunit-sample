using System;

namespace WeatherService
{
    public class WeatherDto
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int GetTemperatureF() => 32 + (int) (TemperatureC / 0.5556);

        public string Summary { get; set; } = "";
    }
}