using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using N5.Telemetry.Observability; // Asegúrate de tener el namespace correcto

namespace N5.User.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly TelemetryTracker _telemetryTracker;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, TelemetryTracker telemetryTracker)
        {
            _logger = logger;
            _telemetryTracker = telemetryTracker;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            // Iniciar la actividad de rastreo
            using (var activity = _telemetryTracker.TrackActivity("GetWeatherForecast"))
            {
                // Agregar eventos a la actividad
                _telemetryTracker.TrackEvent("WeatherForecastRequestReceived", activity);

                // Simular la lógica de negocio
                var forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = Random.Shared.Next(-20, 55),
                    Summary = Summaries[Random.Shared.Next(Summaries.Length)]
                }).ToArray();

                // Agregar métrica personalizada
                _telemetryTracker.TrackMetric("WeatherForecastCount", forecasts.Length);

                // Registrar un evento indicando que el procesamiento ha terminado
                _telemetryTracker.TrackEvent("WeatherForecastResponseGenerated", activity, new Dictionary<string, object>
                {
                    { "forecastCount", forecasts.Length }
                });

                return forecasts;
            }
        }
    }
}
