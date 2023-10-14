using AutoMapper;
using MediatR;
using N5.Eda.Interfaces;
using N5.Event.User;
using N5.User.Domain.DTO;
using N5.User.Domain.Interfaces.Persistence;

namespace N5.User.Application.UseCases.Permission;

public class ModifyUserPermissionHandler : IRequestHandler<ModifyPermissionUseCase, ModifyPermissionCompleteDTO>
{
    private readonly IBroker _broker;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ModifyUserPermissionHandler(IBroker broker, IUnitOfWork workToUnit, IMapper mapper)
        => (_broker, _unitOfWork, _mapper) = (broker, workToUnit, mapper);

    public async Task<ModifyPermissionCompleteDTO> Handle(ModifyPermissionUseCase request, CancellationToken cancellationToken)
    {
        if (request.Request is null)
            throw new ArgumentNullException(nameof(request.Request));

        var commentEntity = _mapper.Map<Domain.Entities.UserPermission>(request.Request);
        commentEntity.Enabled = true;        
        await _unitOfWork.UserPermissionRepository.UpdateAsync(commentEntity, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);

        var result = new ModifyPermissionCompleteDTO()
        {
            IdSession = request.Request.IdSession,
            Id = commentEntity.Id,
        };

        await _broker.Publish(
           EventUser.ModifyPermissionComplete,
           result
       );

        return result;
    }
}