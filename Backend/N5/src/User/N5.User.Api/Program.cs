using OpenTelemetry.Metrics;
using Serilog;
using Serilog.Exceptions;
using System.Globalization;
using System.Reflection;
using N5.Eda.Kafka.Extensions;
using N5.Eda.RequestReply.Extension;
using N5.Event.User;
using N5.Telemetry.Observability;
using N5.User.Api.Middleware;

var AppName = "N5.User.Api";

var builder = WebApplication.CreateBuilder(args);
// Configurar HTTPS
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(7005, listenOptions =>
    {
        listenOptions.UseHttps(); // Asegúrate de que esté configurado para HTTPS
    });
});
// Configuración de Serilog
var logger = new LoggerConfiguration()
    .MinimumLevel.Verbose()
    .Enrich.WithProperty("ApplicationContext", AppName)
    .Enrich.WithProperty("Environment", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Develop")
    .Enrich.WithExceptionDetails()
    .Enrich.FromLogContext()
    .WriteTo.Console();

Log.Logger = logger.ReadFrom.Configuration(builder.Configuration).CreateLogger();

try
{
    Log.Information("Configuring Web Host ({ApplicationContext})...", AppName);

    // Configuración de servicios
    CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddKafka(builder.Configuration, Assembly.GetEntryAssembly());
    builder.Services.AddRequestReply(builder.Configuration);
    builder.Services.AddHealthChecks();
    builder.Services.AddSingleton<WebSocketConnectionManager>();
    builder.Services.AddSingleton<SocketHandler>();

    // Configuración de OpenTelemetry
    builder.Services.AddOpenTelemetryServices(builder.Configuration);

    var app = builder.Build();
    app.MapHealthChecks("/healthz");

  
    
    // Configuración del pipeline
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors(builder => builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    app.UseRequestReply(options =>
    {
        options.RegisterTopics(new string[] {
            EventUser.CreatePermissionComplete,
            EventUser.CreatePermission,
            EventUser.GetPermission,
            EventUser.GetPermissionComplete,
            EventUser.ModifyPermission,
            EventUser.ModifyPermissionComplete,
        });
    });

    app.Map("/ws", SocketHandler.Map);
    app.UseBrokerKafka();
    app.MapControllers();
    app.UseOpenTelemetryPrometheusScrapingEndpoint();
    Log.Information("Starting Web Host ({ApplicationContext})...", AppName);
  
    app.Run();
}
catch (Exception e)
{
    Log.Fatal(e, "Program terminated unexpectedly ({ApplicationContext})!", AppName);
}
finally
{
    Log.CloseAndFlush();
}

public partial class ProgramApi { }
