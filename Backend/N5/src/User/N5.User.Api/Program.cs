using N5.Eda.RequestReply.Extension;
using N5.Event.User;
using N5.Eda.Kafka.Extensions;
using Newtonsoft.Json.Converters;
using Serilog;
using Serilog.Exceptions;
using System.Globalization;
using System.Reflection;
using N5.User.Api.Middleware;
using MongoDB.Driver;
using System.Net.WebSockets;
using System.Net;
using System.Text;

var AppName = "N5.User.Api";

var builder = WebApplication.CreateBuilder(args);

var logger = new LoggerConfiguration()
.MinimumLevel.Verbose()
     .Enrich.WithProperty("ApplicationContext", AppName)
     .Enrich.WithProperty("Environment", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? Environment.GetEnvironmentVariable("ENV") ?? "Develop")
     .Enrich.WithExceptionDetails()
     .Enrich.FromLogContext()
     .WriteTo.Console();

Log.Logger = logger.ReadFrom.Configuration(builder.Configuration).CreateLogger();

try
{
    Log.Information("Configuring Web Host ({ApplicationContext})...", AppName);

    // Add services to the container.

    CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
    builder.Services.AddControllers()
        //.AddNewtonsoftJson(jsonOptions =>
        //{
        //    jsonOptions.SerializerSettings.Converters.Add(new StringEnumConverter());
        //})
        ;
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddKafka(builder.Configuration, Assembly.GetEntryAssembly());
    builder.Services.AddRequestReply(builder.Configuration);
    builder.Services.AddHealthChecks();
    //builder.Services.AddSingleton<ChatWebSocketHandler>();
    builder.Services.AddSingleton<WebSocketConnectionManager>();
    builder.Services.AddSingleton<SocketHandler>();

    var app = builder.Build();
    app.MapHealthChecks("/healthz");
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    //TODO: Change when it'll be neccesary.
    app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
    //Add Kafka Conenction
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

public partial class ProgramApi
{ }