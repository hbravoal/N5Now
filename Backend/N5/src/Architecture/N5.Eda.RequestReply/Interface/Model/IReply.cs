namespace N5.Eda.RequestReply.Interface.Model;

public interface IReply : IRequestReplyContainer
{
    public string IdSession { get; set; }

    string? Id { get; set; }

    string Topic { get; set; }

    string Payload { get; set; }

    DateTime DateCreate { get; set; }
}