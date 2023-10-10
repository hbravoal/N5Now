using Microsoft.Extensions.Hosting;
using N5.Eda.RequestReply.Exceptions;
using N5.Eda.RequestReply.Internal.Model;
using N5.Domain.Interfaces.DTO;
using N5.Eda.Interfaces;
using N5.Eda.RequestReply.Commons;
using N5.Eda.RequestReply.Interface;
using N5.Eda.RequestReply.Interface.Persistence;
using Newtonsoft.Json;
using System.Diagnostics;

namespace N5.Eda.RequestReply.Internal;

public class RequestReplyExecute : IRequestReplyExecute
{
    private readonly IReplyContainerRepository _replyRepository;
    private readonly IBroker _broker;
    private readonly Stopwatch _timeExecute;
    private readonly IHostEnvironment _environment;

    public RequestReplyExecute(IBroker broker, IReplyContainerRepository replyRepository, IHostEnvironment environment)
        => (_replyRepository, _broker, _timeExecute, _environment) = (replyRepository, broker, new(), environment);

    public async Task<T> Wait<T>(IPayloadMessage payload, string startTopic, string endTopic, TimeSpan? timeout = null) where T : class, IPayloadMessage
    {
        if (string.IsNullOrEmpty(payload.IdSession))
            payload.IdSession = RequestReplyCommon.GenerateId();

        if (timeout == null)
            timeout = new TimeSpan(0, 0, 20);

        await _broker.Publish(startTopic, payload);
        if (_environment.IsDevelopment()) Console.WriteLine("[RequestReply:Start - {0}]: Send message to topic {1}", payload.IdSession, startTopic);

        var producer = new Producer() { Id = RequestReplyCommon.GenerateId(), IdSession = payload.IdSession, Topic = startTopic };
        //await _producerRepository.CreateProducerAsync(producer);
        T? response = null;
        try
        {
            bool succeeded = false;
            _timeExecute.Start();
            while (!succeeded)
            {
                if (await IsTimeout(timeout.Value))
                {
                    if (_environment.IsDevelopment()) Console.WriteLine("[RequestReply:Timeout - {0}]: Timeout message to topic {1}", payload.IdSession, endTopic);
                    throw new ExceptionRequestReply("Timeout Session " + producer.IdSession, producer.IdSession);
                }

                //var reply = await _replyRepository.GetReplyByIdSessionAsync(producer.IdSession, endTopic);
                if (await _replyRepository.HasReplyByIdSessionAsync(producer.IdSession, endTopic) > 0)
                {
                    var reply = await _replyRepository.GetReplyByIdSessionAsync(producer.IdSession, endTopic);
                    response = JsonConvert.DeserializeObject<T>(reply.Payload);
                    succeeded = true;
                    if (_environment.IsDevelopment()) Console.WriteLine("[RequestReply:Finish - {0}]: Message confirm to topic {1} ({2}ms)", payload.IdSession, endTopic, _timeExecute.ElapsedMilliseconds);
                }
                else
                    await Task.Delay(50);


            }
            _timeExecute.Stop();
        }
        catch (Exception ex)
        {
            throw new ExceptionRequestReply($"Error Request-Reply ({producer.IdSession}) {ex.Message}", ex, producer.IdSession);
        }
        finally
        {
            _replyRepository.DeleteReplyAsync(producer.IdSession);
        }

        if (response == null) throw new ExceptionRequestReply($"Reply payload is null ({producer.IdSession})");
        return response;
    }

    private async Task<bool> IsTimeout(TimeSpan timeout)
        => _timeExecute.ElapsedMilliseconds >= timeout.TotalMilliseconds;
}