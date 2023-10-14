using N5.Domain.Interfaces.DTO;


public record DefaultResponseDto : IPayloadMessage, IErrorManageMessage
{
    public DefaultResponseDto() { }
    public DefaultResponseDto(string idSession) => (IdSession) = (idSession);

    public string IdSession { get; set; }
    public string Error { get; set; } = string.Empty;
    public int ErrorCode { get; set; }

    public string NameOperation { get; }
}

public static class DefaultResponseDtoExtend
{
    public static DefaultResponseDto GetDefault<T>(this T payload) where T : IPayloadMessage
        => new(payload.IdSession);
}