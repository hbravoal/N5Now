namespace N5.Eda.RequestReply.Exceptions;

public class ExceptionRequestReply : Exception
{
    public ExceptionRequestReply(string idSession)
    {
        IdSession = idSession;
    }

    public ExceptionRequestReply(string? message, string idSession)
        : base(message)
    {
        IdSession = idSession;
    }

    public ExceptionRequestReply(string? message, Exception? innerException, string idSession)
        : base(message, innerException)
    {
        IdSession = idSession;
    }

    public string IdSession { get; set; }
}