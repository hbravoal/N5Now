using N5.Domain.Interfaces.DTO;

namespace N5.User.Application.Exceptions;

public class GenericMediatException : Exception
{
    public int ErrorCode { get; set; }
    public dynamic Request { get; set; } = string.Empty;
    public dynamic Response { get; set; } = string.Empty;

    public GenericMediatException()
    { }

    public GenericMediatException(string message)
        : base(message)
    { }

    public GenericMediatException(string message, dynamic request)
        : base(message) => Request = request;
}

/// <summary>
/// Create Deposit Exception
/// </summary>
public class GenericMediatException<TRequest> : GenericMediatException
    where TRequest : class, IPayloadMessage
{
    public new TRequest? Request
    {
        get => (TRequest)base.Request;
        set => base.Request = value;
    }

    public GenericMediatException()
    { }

    public GenericMediatException(string message)
        : base(message)
    { }

    public GenericMediatException(string message, TRequest request)
        : base(message, request) { }
}