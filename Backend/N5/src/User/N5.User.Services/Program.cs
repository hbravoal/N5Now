using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using N5.Eda.Interfaces;
using N5.Eda.Kafka.Extensions;
using N5.User.Application;
using N5.User.Infrastructure.Persistence;
using Serilog;
using Serilog.Exceptions;
using System.Globalization;
using System.Reflection;

string AppName = "N5.User.Services";

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddCommandLine(args)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
    .AddEnvironmentVariables()
    .Build();

var logger = new LoggerConfiguration()
.MinimumLevel.Verbose()
     .Enrich.WithProperty("ApplicationContext", AppName)
     .Enrich.WithProperty("Environment", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? Environment.GetEnvironmentVariable("ENV") ?? "Develop")
     .Enrich.WithExceptionDetails()
     .Enrich.FromLogContext()
     .WriteTo.Console();

Log.Logger = logger.ReadFrom.Configuration(configuration).CreateLogger();

try
{
    Log.Information("Configuring Web Host ({ApplicationContext})...", AppName);
    CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-US");
    var host = Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration(builder =>
        {
            builder.AddConfiguration(configuration);
        })
        .ConfigureServices((context, services) =>
        {
            //TODO: Change for Invertion of control  Desing principe.
            services.AddKafka(configuration, Assembly.GetEntryAssembly())
                    .AddApplication()
                    //.AddDomain()
                    .AddPersistence(context.Configuration);
        })
        .Build();

    host.UseBrokerKafka();

    var dispacher = host.Services.GetService<IBroker>();
    ArgumentNullException.ThrowIfNull(dispacher);
    dispacher.Build();
    //TODO: When solve  memory database/
    //host.ExecuteMigration(args);

    Log.Information("Starting Web Host ({ApplicationContext})...", AppName);
    host.Run();
}
catch (Exception e)
{
    Log.Fatal(e, "Program terminated unexpectedly ({ApplicationContext})!", AppName);
}
finally
{
    Log.CloseAndFlush();
}

public partial class ProgramService
{ }