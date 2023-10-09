using N5.Eda.RequestReply.Interface.Model;

namespace N5.Eda.RequestReply.Interface.Persistence;

public interface IReplyContainerRepository
{
    Task CreateReplyAsync(IReply reply);

    Task<IReply> GetReplyByIdSessionAsync(string idSession, string topic);

    Task<long> HasReplyByIdSessionAsync(string idSession, string topic);

    Task DeleteReplyAsync(string idSession);
}