namespace N5.User.Domain.Application;

public class ErrorManage
{
    public ErrorManage(string topic, int errorCode, Type payloadType, Type exceptionType)
        => (Topic, ErrorCode, PayloadType, ExceptionType) = (topic, errorCode, payloadType, exceptionType);

    public string Topic { get; set; }
    public int ErrorCode { get; set; }
    public Type PayloadType { get; set; }
    public Type ExceptionType { get; set; }
}