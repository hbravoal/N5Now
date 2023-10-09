using Microsoft.Extensions.DependencyInjection;
using N5.Eda.Interfaces;

namespace N5.Eda
{
    /// <summary>
    /// Base Broker create
    /// </summary>
    public abstract class BrokerProvider : IBroker
    {
        /// <summary>
        /// Service Provider Dependency Injection
        /// </summary>
        protected readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// List Handlers
        /// </summary>
        private readonly IBrokerHandlerList _brokerHandlerList;

        /// <summary>
        /// Constructor basic
        /// </summary>
        /// <param name="serviceProvider">Service Provider Dependency Injection</param>
        public BrokerProvider(IServiceProvider serviceProvider, IBrokerHandlerList brokerHandlerList)
        {
            _serviceProvider = serviceProvider;
            _brokerHandlerList = brokerHandlerList;
        }

        /// <summary>
        /// Build All event to suscribe
        /// </summary>
        public virtual void Build()
        {
            IBrokerDispatcher instance = _serviceProvider.GetService<IBrokerDispatcher>();
            ArgumentNullException.ThrowIfNull(instance);
            //foreach (var eventKafka in _brokerHandlerList.GetEvents())instance.Build(eventKafka.Key, eventKafka.Value);
            instance.Subscribe();
        }

        /// <summary>
        /// Send message to broker
        /// </summary>
        /// <param name="topic">Topic Name</param>
        /// <param name="payload">Message</param>
        /// <returns></returns>
        public abstract Task Publish(string topic, object payload);
    }
}