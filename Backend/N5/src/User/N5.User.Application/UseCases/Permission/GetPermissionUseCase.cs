using FluentValidation;
using N5.User.Domain.Application;
using N5.Event.User;
using N5.User.Application.Exceptions.ValidatableExtension;
using N5.User.Domain.DTO;
using Throw;
using N5.User.Application.Exceptions;
using N5.User.Application.Exceptions.Permission;

namespace N5.User.Application.UseCases.Permission;

public class GetPermissionUseCase : UserUseCase<GetPermissionDto, GetPermissionCompleteDTO>
{
    public GetPermissionUseCase(GetPermissionDto request)
        => (
            Request,
            ValidationManage,
            ErrorManage,
            IsValid,
            ValidationType
        ) = (
            request,
            new(EventUser.GetPermissionComplete, 1, typeof(GetPermissionCompleteDTO), typeof(GetPermissionException)),
            new(EventUser.GetPermissionComplete, 1, typeof(GetPermissionCompleteDTO), typeof(GetPermissionException)),
            () => Validators is not null && Validators.Any() ?
                Request.ThrowIfNull((errorMessage) => new GenericMediatException<GetPermissionDto>(errorMessage, request)).IfNotValid(Validators).Value is not null : true,
            typeof(IEnumerable<IValidator<GetPermissionDto>>)
        );
}