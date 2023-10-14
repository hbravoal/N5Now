using MediatR;
using N5.Eda;
using N5.Eda.Attributes;
using N5.Eda.Interfaces;
using N5.Event.User;
using N5.User.Application.UseCases.Permission;
using N5.User.Domain.DTO;
using N5.Utilities;

namespace N5.User.Services.Handlers.UserPermission;

[BrokerTopicHandler(EventUser.ModifyPermission)]
public class ModifyUserPermission : BrokerHandler, IBrokerEvent
{
    /// <summary>
    /// Mediator Instance
    /// </summary>
    private readonly IMediator _mediator;

    /// <summary>
    /// Constructor base
    /// </summary>
    /// <param name="configuration">Configuration EDA provider</param>
    /// <param name="mediator">mediator instance from MediatR</param>
    public ModifyUserPermission(IBrokerConfiguration configuration, IMediator mediator)
        : base(configuration)
        => _mediator = mediator;

    /// <summary>
    /// Execute handler whem generate publish
    /// </summary>
    /// <param name="payload">payload message</param>
    /// <param name="token">cancel Token</param>
    /// <returns></returns>
    public override async Task Handler(IBrokerPayload payload, CancellationToken token)
        => await _mediator.Send(
            new ModifyPermissionUseCase(payload.Value.ToDeserializeJSON<ModifyPermissionDto>())
        );
}