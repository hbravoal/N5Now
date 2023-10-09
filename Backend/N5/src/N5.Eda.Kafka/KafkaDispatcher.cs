using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using N5.Eda;
using N5.Eda.Interfaces;
using N5.Eda.Kafka.Model;

namespace N5.Eda.Kafka;

/// <summary>
/// Dispacher for Kafka
/// </summary>
public class KafkaDispatcher : BrokerDispatcher
{
    private readonly IAdminClient _adminClient;
    private readonly IHostEnvironment _environment;
    private readonly IConsumer<Ignore, string> _consumer;

    /// <summary>
    /// List Handlers
    /// </summary>
    private readonly IBrokerHandlerList _brokerHandlerList;


    /// <summary>
    /// Constructor basic
    /// </summary>
    /// <param name="configurationBroker">Configuration Broker</param>
    /// <param name="serviceProvider">Service Dependency Injection</param>
    /// <param name="configurationSetting">AppSetting information</param>
    public KafkaDispatcher(
        IServiceProvider serviceProvider,
        IAdminClient adminClient,
        IConsumer<Ignore, string> consumer,
        IHostEnvironment environment,
        IBrokerHandlerList brokerHandlerList)
        : base(serviceProvider)
    => (_adminClient, _consumer, _environment, _brokerHandlerList) = (adminClient, consumer, environment, brokerHandlerList);



    /// <summary>
    /// Create Topic
    /// </summary>
    /// <returns>Task Executed</returns>
    public override Task CreateTopic(string topic)
        => Task.Factory.StartNew(() =>
        {
            try
            {
                _adminClient.GetMetadata(topic, new TimeSpan(0, 1, 0));
            }
            catch (CreateTopicsException e)
            {
                Console.WriteLine($"An error occured creating topic {e.Results[0].Topic}: {e.Results[0].Error.Reason}");
            }
        });

    /// <summary>
    /// Create Suscribe service, when publish event, this method generate handler
    /// </summary>
    /// <returns>Task Executed</returns>
    public override Task Subscribe()
     => Task.Factory.StartNew(() =>
     {
         string[] topics = _brokerHandlerList.GetEvents().Select(x => x.Key).ToArray();
         foreach (var topic in topics) CreateTopic(topic);

         bool cancelled = false;
         CancellationToken cancellationToken = new();
         _consumer.Subscribe(topics);

         Console.WriteLine("**************************");
         Console.WriteLine("Suscribed Topics: \n" + string.Join("\n", topics));
         Console.WriteLine("**************************");

         while (!cancelled)
         {
             var consumeResult = _consumer.Consume(cancellationToken);
             foreach (var itemEvent in _brokerHandlerList.GetEvents().Where(x => x.Key == consumeResult.Topic).SelectMany(x => x.Value))
             {
                 try
                 {
                     var instance = _serviceProvider.GetService(itemEvent) as IBrokerEvent;
                     ArgumentNullException.ThrowIfNull(instance);
                     instance.Handler(new MessageKafka(consumeResult), cancellationToken);
                     if (_environment.IsDevelopment()) Console.WriteLine("[Kafka:suscribe - {0}] for handler {1} recive message: {2}", consumeResult.Topic, itemEvent.GetType().Name, consumeResult.Message.Value);
                 }
                 catch (Exception ex)
                 {
                     //TODO: Change to logger
                     Console.WriteLine("Error " + ex.Message);
                 }
             }
         }

         _consumer.Close();
     });
}