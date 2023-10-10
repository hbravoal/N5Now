using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using N5.Eda.RequestReply.Interface.Model;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]
[assembly: InternalsVisibleTo("N5.Eda.RequestReply.Test")]

namespace N5.Eda.RequestReply.Internal.Model;

internal class Producer : IProducer
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string IdSession { get; set; }

    public string Topic { get; set; }
}