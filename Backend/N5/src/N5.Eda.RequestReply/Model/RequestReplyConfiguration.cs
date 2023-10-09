using N5.Eda.Interfaces;
using N5.Eda.Model;
using N5.Eda.RequestReply.Services;

namespace N5.Eda.RequestReply.Model;

public class RequestReplyConfiguration : EdaConfiguration
{
    public RequestReplyConfiguration(IBrokerHandlerList brokerHandlerList)
        : base(brokerHandlerList)
    {
    }

    public void RegisterTopics(string[] topics)
        => AddServices<ServiceRequestReply>(topics);
}