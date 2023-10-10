using N5.Eda.RequestReply.Extension;
using N5.Event.User;
using N5.Eda.Kafka.Extensions;
using Newtonsoft.Json.Converters;
using Serilog;
using Serilog.Exceptions;
using System.Globalization;
using System.Reflection;

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

    var app = builder.Build();
    app.MapHealthChecks("/healthz");
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    //Add Kafka Conenction
    app.UseRequestReply(options =>
    {
        options.RegisterTopics(new string[] {
            EventUser.CreatePermissionComplete,
            //EventRealty.GetCommentsComplete,
        });
    });
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