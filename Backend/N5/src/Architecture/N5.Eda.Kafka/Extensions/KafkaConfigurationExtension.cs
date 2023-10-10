using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using N5.Eda.Interfaces;
using N5.Eda.Kafka.Configurations;

namespace N5.Eda.Kafka.Extensions;

/// <summary>
/// Extend configuration
/// </summary>
public static class KafkaConfigurationExtension
{
    /// <summary>
    /// Create Confiiguration Kafka
    /// </summary>
    /// <param name="value">Configuration Broker</param>
    /// <param name="configurationSettings">Setting Configuration</param>
    /// <returns>Configuration Kafka</returns>
    /// <example>
    /// _configurationEda.GetKafkaConfiguration(_configurationSetting)
    /// </example>
    public static ConsumerConfig GetKafkaConfiguration(this IBrokerConfiguration value, IConfiguration configurationSettings)
    {
        var setting = configurationSettings.GetRequiredSection("Kafka").Get<KafkaConfiguration>();
        var maxLengthGroupId = 248;
        string groupId = $"{setting.GroupId}_{Guid.NewGuid()}";
        if (groupId.Length > maxLengthGroupId)
        {
            groupId = groupId.Substring(0, 248);
        }
        var config = new ConsumerConfig
        {
            BootstrapServers = setting.StrapServers,
            GroupId = groupId,
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        if (setting?.MessageMaxBytes != 0)
        {
            config.MessageMaxBytes = setting.MessageMaxBytes;
            config.MaxPartitionFetchBytes = setting.MessageMaxBytes;
        }

        return config;
    }
}