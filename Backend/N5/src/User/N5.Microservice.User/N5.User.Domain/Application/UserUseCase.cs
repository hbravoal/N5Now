using FluentValidation;
using N5.Domain.Interfaces.DTO;
using N5.User.Domain.Interfaces.Application;

namespace N5.User.Domain.Application;

public class UserUseCase<TRequest, TResponse> : IUserUseCase<TRequest, TResponse>
    where TRequest : IPayloadMessage
{
    public TRequest Request { get; set; }

    object IUserUseCase<TResponse>.Request
    {
        get => Request;
        set => Request = (TRequest)value;
    }

    public ErrorManage? ValidationManage { get; set; }
    public ErrorManage? ErrorManage { get; set; }
    public IEnumerable<IValidator<TRequest>>? Validators { get; set; }
    public Type? ValidationType { get; set; }
    public Func<bool>? IsValid { get; set; }
}