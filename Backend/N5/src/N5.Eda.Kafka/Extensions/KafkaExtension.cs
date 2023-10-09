using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
//using MongoDB.Driver;
using N5.Eda.Interfaces;
using N5.Eda.Kafka.Configurations;
using N5.Eda.Kafka.Extensions;
using System.Reflection;
//using MongoDB.Driver;
using N5.Eda.Kafka.Model;
using N5.Eda.Extensions;

namespace N5.Eda.Kafka.Extensions;

/// <summary>
/// Dependency Injection kafka
/// </summary>
public static class KafkaExtension
{
    /// <summary>
    /// Add Application
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    /// <example>
    /// //add to program
    /// services.UseKafka();
    /// //add to DI
    /// services.AddKafka(Assembly.GetEntryAssembly());
    /// </example>
    public static IServiceCollection AddKafka(this IServiceCollection services, IConfiguration configuration, params Assembly[] assemblies)
    {
        //Base
        services.AddMessageBroker(assemblies);
        services.AddSingleton<IBroker, Kafka>();
        services.AddTransient<IBrokerConfiguration, KafkaConfiguration>();
        services.AddTransient<IBrokerDispatcher, KafkaDispatcher>();

        //Database
        //services.AddSingleton<IMongoClient>(s => new MongoClient(configuration.GetConnectionString("RequestReply")));

        //Init Kafka
        services.AddTransient<IAdminClient>((service) =>
            {
                return new AdminClientBuilder(new KafkaConfiguration().GetKafkaConfiguration(configuration)).Build();
            });

        services.AddTransient<IConsumer<Ignore, string>>((service) =>
        {
            return new ConsumerBuilder<Ignore, string>(new KafkaConfiguration().GetKafkaConfiguration(configuration)).Build();
        });

        services.AddTransient<IProducer<Null, string>>((service) =>
        {
            return new ProducerBuilder<Null, string>(new KafkaConfiguration().GetKafkaConfiguration(configuration)).Build();
        });

        return services;
    }

    /// <summary>
    /// User Broker Kafka for build broker and dispacher
    /// </summary>
    /// <param name="app">Host information you can Web or just Host</param>
    /// <returns>Host modify</returns>
    public static IHost UseBrokerKafka(this IHost app, Action<KafkaOptions> kafkaConfiguration = null)
    {
        var kafkaOptions = new KafkaOptions(app.Services.GetRequiredService<IBrokerHandlerList>());

        if (kafkaConfiguration != null)
            kafkaConfiguration(kafkaOptions);

        app.UseMessageBroker(options =>
        {
            options = kafkaOptions;
        });

        var dispacher = app.Services.GetService<IBroker>();
        ArgumentNullException.ThrowIfNull(dispacher);
        dispacher.Build();

        return app;
    }
}