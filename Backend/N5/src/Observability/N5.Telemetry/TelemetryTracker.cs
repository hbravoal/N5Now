using OpenTelemetry.Trace;
using OpenTelemetry.Metrics;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;
using System.Diagnostics.Metrics;

namespace N5.Telemetry.Observability;

public class TelemetryTracker
{
    private readonly ActivitySource _activitySource;
    private readonly Meter _meter;
    private readonly ILogger<TelemetryTracker> _logger;

    // Diccionario para almacenar y evitar duplicados en los contadores
    private static readonly ConcurrentDictionary<string, Counter<long>> _counters = new();

    public TelemetryTracker(TracerProvider tracerProvider, ILogger<TelemetryTracker> logger)
    {
        _activitySource = new ActivitySource("serviceApp");  // Inicializar el ActivitySource
        _meter = new Meter("serviceApp");  // Creación del meter para las métricas
        _logger = logger;
    }

    /// <summary>
    /// Inicia y registra una actividad de OpenTelemetry.
    /// </summary>
    public Activity TrackActivity(string activityName, Dictionary<string, object> attributes = null, Action<Activity> action = null)
    {
        var activity = Activity.Current;

        if (activity == null) return null;

        // Agregar los atributos pasados al método (si los hay)
        if (attributes != null)
        {
            foreach (var attribute in attributes)
            {
                activity.SetTag(attribute.Key, attribute.Value);
            }
        }

        // Ejecutar cualquier acción adicional
        action?.Invoke(activity);

        activity.SetEndTime(DateTime.UtcNow);
        activity.Stop();

        return activity;
    }


    /// <summary>
    /// Registra un evento dentro de una actividad actual de OpenTelemetry.
    /// </summary>
    public void TrackEvent(string eventName, Activity activity, Dictionary<string, object> eventAttributes = null)
    {
        if (activity == null || activity.IsAllDataRequested == false)
        {
            return;
        }

        var attributes = eventAttributes != null ? new ActivityTagsCollection(eventAttributes) : null;

        activity.AddEvent(new ActivityEvent(eventName, DateTime.UtcNow, attributes));

        _logger.LogInformation("Evento registrado: {eventName} con atributos {attributes}", eventName, eventAttributes);
    }



    /// <summary>
    /// Registra una métrica personalizada en OpenTelemetry.
    /// </summary>
    public void TrackMetric(string metricName, long value, Dictionary<string, string> labels = null)
    {
        var counter = _counters.GetOrAdd(metricName, _meter.CreateCounter<long>(metricName));

        // Si hay etiquetas, las incluimos
        if (labels != null)
        {
            var labelSet = new TagList();
            foreach (var label in labels)
            {
                labelSet.Add(label.Key, label.Value);
            }
            counter.Add(value, labelSet);
        }
        else
        {
            counter.Add(value);  // Sin etiquetas
        }

        _logger.LogInformation("Métrica registrada: {metricName} con valor {value} y etiquetas {labels}", metricName, value, labels);
    }

}
