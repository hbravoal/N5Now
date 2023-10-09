namespace N5.Eda.Interfaces;

/// <summary>
/// Basic infomation configuration kafka
/// </summary>
public interface IBrokerConfiguration
{
    /// <summary>
    /// Server name, you can use many server divide to ;
    /// </summary>
    string StrapServers { get; set; }

    /// <summary>
    /// Group Id to consumers
    /// </summary>
    string GroupId { get; set; }
}