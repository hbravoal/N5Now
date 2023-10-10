using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using N5.Eda.Interfaces;
using N5.Utilities;

namespace N5.Eda.Kafka;

/// <summary>
/// Kafka implementation for Miles
/// based to https://www.nuget.org/packages/Confluent.Kafka/
/// </summary>
/// <remarks>Validate for implementation Dependecy Injection</remarks>
/// <example>
/// </example>
public class Kafka : BrokerProvider
{
    private readonly IProducer<Null, string> _producer;
    private readonly IHostEnvironment _environment;

    /// <summary>
    /// Constructor base
    /// </summary>
    /// <param name="configurationBroker">Configuration Broker</param>
    /// <param name="serviceProvider">Service Dependency Injection</param>
    /// <param name="configurationSettings">Information Appsettings</param>
    public Kafka(
        IServiceProvider serviceProvider,
        IBrokerHandlerList brokerHandlerList,
        IProducer<Null, string> producer,
        IHostEnvironment environment) 
        : base(serviceProvider, brokerHandlerList)
    => (_producer, _environment) = (producer, environment);


    /// <summary>
    /// Send message Kafka
    /// </summary>
    /// <param name="topic">Topic Name</param>
    /// <param name="payload">Message</param>
    /// <returns></returns>
    public override Task Publish(string topic, object payload)
     => Task.Factory.StartNew(async () =>
     {
         await _producer.ProduceAsync(topic, new Message<Null, string> { Value = payload.ToSerializeJSON() });
         if (_environment.IsDevelopment()) Console.WriteLine("[Kafka:publish - {0}] Send message: {1}", topic, payload.ToSerializeJSON());
     });
}