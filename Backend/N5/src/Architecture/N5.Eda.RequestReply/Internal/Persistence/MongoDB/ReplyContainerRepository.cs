using MongoDB.Driver;
using N5.Eda.RequestReply.Interface.Model;
using N5.Eda.RequestReply.Interface.Persistence;
using N5.Eda.RequestReply.Internal.Model;
using N5.Eda.RequestReply.Internal.Persistence.MongoDB.Context;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("N5.Eda.RequestReply.Test")]

namespace N5.Eda.RequestReply.Internal.Persistence.MongoDB;

internal class ReplyContainerRepository : IReplyContainerRepository
{
    private readonly RequestReplyContext _context;

    public ReplyContainerRepository(RequestReplyContext context)
    {
        _context = context;
    }

    public Task CreateReplyAsync(IReply reply)
        => _context._replyCollection.InsertOneAsync(reply as Reply);

    public Task DeleteReplyAsync(string idSession)
     => _context._replyCollection.DeleteManyAsync(x => x.IdSession == idSession);
 
    public async Task<IReply> GetReplyByIdSessionAsync(string idSession, string topic)
        => _context._replyCollection.Find(x => x.IdSession == idSession && x.Topic == topic).FirstOrDefault() as IReply;

    public Task<long> HasReplyByIdSessionAsync(string idSession, string topic)
        => Task.Factory.StartNew(() => _context._replyCollection.CountDocuments(x => x.IdSession == idSession && x.Topic == topic));
}