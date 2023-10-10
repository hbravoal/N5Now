using FluentValidation;
using MediatR;
using N5.Domain.Interfaces.DTO;
using N5.User.Domain.Application;

namespace N5.User.Domain.Interfaces.Application;

public interface IUserUseCase<TResponse> : IRequest<TResponse>
{
    public dynamic Request { get; set; }
    public ErrorManage ValidationManage { get; }
    public ErrorManage ErrorManage { get; }
    public Type ValidationType { get; }
    public Func<bool> IsValid { get; }
}

public interface IUserUseCase<TRequest, TResponse> : IUserUseCase<TResponse>
    where TRequest : IPayloadMessage
{
    public new TRequest Request { get; set; }
    public IEnumerable<IValidator<TRequest>> Validators { get; }
}