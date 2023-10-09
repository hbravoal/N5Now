using N5.Eda.Interfaces;
namespace N5.Eda.Model;

public class EdaConfiguration
{
    private readonly IBrokerHandlerList _brokerHandlerList;

    public EdaConfiguration(IBrokerHandlerList brokerHandlerList)
    {
        _brokerHandlerList = brokerHandlerList;
    }

    public void AddServices(EdaConfigurationService requestReplyService)
        => _brokerHandlerList.Add(requestReplyService.ServiceType, requestReplyService.Topic);

    public void AddServices<T>(string[] topic) where T : IBrokerEvent
        => _brokerHandlerList.Add(typeof(T), topic);
}