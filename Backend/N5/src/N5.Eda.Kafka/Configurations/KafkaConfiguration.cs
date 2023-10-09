
using N5.Eda.Interfaces;
namespace N5.Eda.Kafka.Configurations;

/// <summary>
/// Configuration Kafka
/// </summary>
public class KafkaConfiguration : IBrokerConfiguration
{
    /// <summary>
    /// Server to connect to Kafka
    /// </summary>
    public string StrapServers { get; set; } = string.Empty;

    /// <summary>
    /// Group Consumer to Kafka
    /// </summary>
    public string GroupId { get; set; } = string.Empty;

    /// <summary>
    /// Message Max Bytes to kafka
    /// </summary>
    public int MessageMaxBytes { get; set; } = 0;
}