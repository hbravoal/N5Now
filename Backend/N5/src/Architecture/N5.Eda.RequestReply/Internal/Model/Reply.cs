using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using N5.Eda.RequestReply.Interface.Model;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("N5.Eda.RequestReply.Test")]

namespace N5.Eda.RequestReply.Internal.Model;

internal class Reply : IReply
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string IdSession { get; set; }

    public string Topic { get; set; }

    public string Payload { get; set; }

    public DateTime DateCreate { get; set; }
}