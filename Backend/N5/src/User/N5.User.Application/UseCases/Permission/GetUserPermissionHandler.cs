using AutoMapper;
using MediatR;
using N5.Eda.Interfaces;
using N5.Event.User;
using N5.User.Domain.DTO;
using N5.User.Domain.Interfaces.Persistence;

namespace N5.User.Application.UseCases.Permission;

public class GetUserPermissionHandler : IRequestHandler<GetPermissionUseCase, GetPermissionCompleteDTO>
{
    private readonly IBroker _broker;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetUserPermissionHandler(IBroker broker, IUnitOfWork workToUnit, IMapper mapper)
        => (_broker, _unitOfWork, _mapper) = (broker, workToUnit, mapper);

    public async Task<GetPermissionCompleteDTO> Handle(GetPermissionUseCase request, CancellationToken cancellationToken)
    {
        if (request.Request is null)
            throw new ArgumentNullException(nameof(request.Request));

        var response = await _unitOfWork.UserPermissionRepository.GetPaginingAsync(d => d.Enabled == true, request.Request.Page, request.Request.PageSize);
        //TODO Mapper:
        var responseCastedf = _mapper.Map<List<PermissionDto>>(response);
        var result = new GetPermissionCompleteDTO()
        {
            IdSession = request.Request.IdSession,
            Permissions = responseCastedf,
        };

        await _broker.Publish(
           EventUser.GetPermissionComplete,
           result
       );

        return result;
    }
}