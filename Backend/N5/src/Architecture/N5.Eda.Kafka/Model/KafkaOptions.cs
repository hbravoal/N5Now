using N5.Eda.Interfaces;
using N5.Eda.Model;

namespace N5.Eda.Kafka.Model
{
    public class KafkaOptions : EdaConfiguration
    {
        public KafkaOptions(IBrokerHandlerList brokerHandlerList)
            : base(brokerHandlerList)
        {
        }
    }
}