using MongoDB.Driver;
using N5.Eda.RequestReply.Internal.Model;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("N5.Eda.RequestReply.Test")]

namespace N5.Eda.RequestReply.Internal.Persistence.MongoDB.Context;

internal class RequestReplyContext
{
    public readonly IMongoCollection<Producer> _producerCollection;
    public readonly IMongoCollection<Reply> _replyCollection;

    public RequestReplyContext(IMongoClient client)
    {
        var mongoDatabase = client.GetDatabase("RequestReply");

        _producerCollection = mongoDatabase.GetCollection<Producer>("Producer");
        _replyCollection = mongoDatabase.GetCollection<Reply>("Reply");
    }
}