using N5.Eda.Interfaces;
using N5.Eda.RequestReply.Commons;
using N5.Eda.RequestReply.Exceptions;
using N5.Eda.RequestReply.Interface.Persistence;
using N5.Eda.RequestReply.Internal.Model;
using Newtonsoft.Json.Linq;

namespace N5.Eda.RequestReply.Services;

public class ServiceRequestReply : BrokerHandler, IBrokerEvent
{
    private readonly IReplyContainerRepository _replyRepository;

    public ServiceRequestReply(IBrokerConfiguration configuration, IReplyContainerRepository replyRepository)
           : base(configuration)
    {
        _replyRepository = replyRepository;
    }

    public override async Task Handler(IBrokerPayload result, CancellationToken token)
    {
        JObject o = JObject.Parse(result.Value);
        var session = o["idSession"]?.Value<string>();

        if (string.IsNullOrEmpty(session))
            throw new ExceptionRequestReply($"Not Containt IdSession - Call: {result.Topic} payload: {result.Value}", "");

        _replyRepository.CreateReplyAsync(new Reply() { Id = RequestReplyCommon.GenerateId(), IdSession = session, Topic = result.Topic, Payload = result.Value, DateCreate = DateTime.UtcNow });
    }
}