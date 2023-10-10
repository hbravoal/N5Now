
namespace N5.Eda.RequestReply.Interface.Model;

public interface IProducer : IRequestReplyContainer
{
    string Id { get; set; }

    public string IdSession { get; set; }

    string Topic { get; set; }
}