using SlugEnt.FluentResults.Extensions.AspNetCore;

namespace SlugEnt.FluentResults.Samples.WebHost
{
    public class WeatherForecastDto
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public string? Summary { get; set; }
    }

    public class MyErrorDto : IErrorDto
    {
        public string? Message { get; set; }

        public string? Path { get; set; }
    }
}