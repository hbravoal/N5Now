using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using N5.Domain.Interfaces.DTO;
using N5.Eda.Interfaces;
using N5.TryCatch.Extend;
using N5.User.Application.Exceptions;
using N5.User.Domain.Interfaces.Application;
using N5.Utilities;

namespace N5.User.Application.Behaviours;

/// <summary>
/// For MediatR and Fluent Validation.
/// Validate after enter to Handler and manage any error
/// </summary>
public class CustomManageBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : class
{
    private readonly IBroker _broker;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<CustomManageBehaviour<TRequest, TResponse>> _logger;

    public CustomManageBehaviour(IBroker broker, IServiceProvider sp, ILogger<CustomManageBehaviour<TRequest, TResponse>> logger)
        => (_broker, _serviceProvider, _logger) = (broker, sp.CreateScope().ServiceProvider, logger);

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        => await this.TryAsync(async () =>
        {
            var prop = request.GetType().GetProperty("Validators");
            var typeValidators = ((dynamic)request).ValidationType as Type;
            if (typeValidators != null)
            {
                var validators = _serviceProvider.GetRequiredService(typeValidators);
                prop?.SetValue(request, validators);
                ((dynamic)request).IsValid();
            }

            _logger.LogInformation(
                $"{Environment.NewLine}Excecute {typeof(TRequest).Name} whit request: " + "{@Request}" + Environment.NewLine,
                ((object)((dynamic)request).Request).ToSerializeJSON()
            );

            return await Task.Run(async () => await next(), cancellationToken);
        })
        .Catch(async (ex) =>
            await Task.Run(() =>
                {
                    try
                    {
                        var _request = request as IUserUseCase<TResponse>;
                        if (_request != null)
                        {
                            IErrorManageMessage? response = null;
                            var idSession = ((IPayloadMessage)_request.Request)?.IdSession ?? string.Empty;
                            var message = $"{Environment.NewLine}Something went wrong excecuing {typeof(TRequest).Name}: {ex.Message.Replace('{', '(').Replace('}', ')')}, Request: " + "{@Request}" + Environment.NewLine;
                            _logger.LogError(ex, message, ((object?)_request.Request)?.ToSerializeJSON());
                            if (typeof(GenericMediatException) == ex.GetType().BaseType)
                            {
                                response = (IErrorManageMessage?)Activator.CreateInstance(((dynamic)request).ValidationManage.PayloadType);
                                if (response is not null)
                                {
                                    var topic = $"{((dynamic)request).ValidationManage.Topic}";
                                    ((IPayloadMessage)response).IdSession = idSession;
                                    response.Error = ex.Message;
                                    response.ErrorCode = ((dynamic)request).ValidationManage.ErrorCode;
                                    _broker.Publish(topic, response).Wait(cancellationToken);
                                }
                            }
                            else if (ex.InnerException?.InnerException is not null && "SqlException" == ex.InnerException.InnerException.GetType().Name
                                && (((dynamic)ex.InnerException.InnerException).Number == 2627 || ((dynamic)ex.InnerException.InnerException).Number == 2601))
                            {
                                response = (IErrorManageMessage?)Activator.CreateInstance(_request.ErrorManage.PayloadType);
                                if (response is not null)
                                {
                                    var topic = $"{((dynamic)request).ErrorManage.Topic}";
                                    ((IPayloadMessage)response).IdSession = idSession;
                                    response.Error = "Cannot create a register because exist another with a same value.";
                                    response.ErrorCode = ((dynamic)request).ErrorManage.ErrorCode;
                                    _broker.Publish(topic, response).Wait(cancellationToken);
                                }
                            }
                            else
                            {
                                response = (IErrorManageMessage?)Activator.CreateInstance(_request.ErrorManage.PayloadType);
                                if (response is not null)
                                {
                                    var topic = $"{_request.ErrorManage.Topic}";
                                    ((IPayloadMessage)response).IdSession = idSession;
                                    response.Error = ex.Message;
                                    response.ErrorCode = _request.ErrorManage.ErrorCode;
                                    _broker.Publish(topic, response).Wait(cancellationToken);
                                }
                            }
                            return response as TResponse;
                        }
                    }
                    catch (Exception execption)
                    {
                        Console.WriteLine($"Exception: {execption}");
                    }
                    return default;
                }, cancellationToken) ?? Activator.CreateInstance<TResponse>()
        )
        .Apply();
}