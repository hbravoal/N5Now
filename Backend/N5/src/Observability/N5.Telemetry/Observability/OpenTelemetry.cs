using OpenTelemetry.Trace;
using OpenTelemetry.Resources;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;

namespace N5.Telemetry.Observability;

public static class OpenTelemetry
{
    private static string? _openTelemetryUrl;
    
  public static void AddTracing(this IServiceCollection services, IConfiguration configuration)
  {
      var otlpEndpoint = configuration.GetValue<string>("OpenTelemetry:OtlpExporterEndpoint");
  
      services.AddOpenTelemetry().WithTracing(builder => builder
          .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("serviceApp"))
          .AddAspNetCoreInstrumentation()
          
          .AddOtlpExporter(exporter =>
          {
              exporter.Endpoint = new Uri(otlpEndpoint ?? "http://localhost:4317");
          })
      );
  }

    public static void AddMetrics(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddOpenTelemetry().WithMetrics(builder => builder
            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("serviceApp"))
            .AddAspNetCoreInstrumentation()
            .AddOtlpExporter(exporter =>
            {
                exporter.Endpoint = new Uri("http://localhost:4317"); 
            })
        );
        
        
    }
    
    public static void AddLog(this IHostBuilder builder, IConfiguration configuration)
    {
        var otlpEndpoint = configuration.GetValue<string>("OpenTelemetry:OtlpExporterEndpoint");

        builder.ConfigureLogging(logging =>
        {
            logging.ClearProviders();
            logging.AddOpenTelemetry(options =>
            {
                options.IncludeFormattedMessage = true;
                options.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("serviceApp"));
                options.AddOtlpExporter(exporter =>
                {
                    exporter.Endpoint = new Uri(otlpEndpoint ?? "http://localhost:4317");
                    exporter.Protocol = OtlpExportProtocol.Grpc;
                });
            });
        });
    }
   

    public static void AddOpenTelemetryServices(this IServiceCollection services, IConfiguration configuration)
    {
        var otlpEndpoint = configuration.GetValue<string>("OpenTelemetry:OtlpExporterEndpoint") ?? "http://localhost:4317";

        // Configuración de rastreo
        services.AddOpenTelemetry()
            .WithTracing(builder => builder
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("serviceApp"))
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddOtlpExporter(exporter =>
                {
                    exporter.Endpoint = new Uri(otlpEndpoint);
                    exporter.Protocol = OtlpExportProtocol.Grpc; 
                }))
            .WithMetrics(builder => builder
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("serviceApp"))
                .AddRuntimeInstrumentation()
                .AddPrometheusExporter()); // Sin Endpoint aquí
    }

    public static void AddLoggingWithOpenTelemetry(this IHostBuilder builder, IConfiguration configuration)
    {
        var otlpEndpoint = configuration.GetValue<string>("OpenTelemetry:OtlpExporterEndpoint") ?? "http://localhost:4317";

        builder.ConfigureLogging(logging =>
        {
            logging.ClearProviders();
            logging.AddOpenTelemetry(options =>
            {
                options.IncludeFormattedMessage = true;
                options.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("serviceApp"));
                options.AddOtlpExporter(exporter =>
                {
                    exporter.Endpoint = new Uri(otlpEndpoint);
                    exporter.Protocol = OtlpExportProtocol.Grpc;
                });
            });
        });
    }
    
}