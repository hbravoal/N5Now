using Confluent.Kafka;
using N5.Eda.Model;

namespace N5.Eda.Kafka.Model
{
    /// <summary>
    /// Basic Message Kafka
    /// </summary>
    public class MessageKafka : BrokerPayload
    {
        /// <summary>
        /// Constructor Message Kafka
        /// </summary>
        /// <param name="message">Response Kafka</param>
        public MessageKafka(ConsumeResult<Ignore, string> message)
        {
            Topic = message.Topic;
            Value = message.Message.Value;
        }
    }
}