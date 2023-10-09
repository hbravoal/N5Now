namespace N5.Eda.RequestReply.Commons;

public static class RequestReplyCommon
{
    public static string GenerateId(int length = 24)
       => Guid.NewGuid().ToString().Replace("-", "").Substring(0, length);
}