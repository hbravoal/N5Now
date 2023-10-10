using AutoMapper;
using MediatR;
using N5.Eda.Interfaces;
using N5.Event.User;
using N5.User.Application.UseCases.Permission;
using N5.User.Domain.DTO;
using N5.User.Domain.Interfaces.Persistence;

namespace N5.User.Application.UseCases.Comment;

public class CreateUserPermissionHandler : IRequestHandler<CreatePermissionUseCase, CreatePermissionCompleteDTO>
{
    private readonly IBroker _broker;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateUserPermissionHandler(IBroker broker, IUnitOfWork workToUnit, IMapper mapper)
        => (_broker, _unitOfWork, _mapper) = (broker, workToUnit, mapper);

    public async Task<CreatePermissionCompleteDTO> Handle(CreatePermissionUseCase request, CancellationToken cancellationToken)
    {
        if (request.Request is null)
            throw new ArgumentNullException(nameof(request.Request));

        var commentEntity = _mapper.Map<Domain.Entities.UserPermission>(request.Request);
        commentEntity.Enabled = true;
        await _unitOfWork.UserPermissionRepository.AddAsync(commentEntity, cancellationToken);
        await _unitOfWork.SaveAsync(cancellationToken);

        var result = new CreatePermissionCompleteDTO()
        {
            IdSession = request.Request.IdSession,
            Id = commentEntity.Id,
            CreatedDate = commentEntity.CreatedDate
        };

        await _broker.Publish(
           EventUser.CreatePermission,
           result
       );

        return result;
    }
}