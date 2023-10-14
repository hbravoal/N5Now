using FluentValidation;
using N5.User.Domain.Application;
using N5.Event.User;
using N5.User.Application.Exceptions.ValidatableExtension;
using N5.User.Domain.DTO;
using Throw;
using N5.User.Application.Exceptions;
using N5.User.Application.Exceptions.Permission;

namespace N5.User.Application.UseCases.Permission;

public class ModifyPermissionUseCase : UserUseCase<ModifyPermissionDto, ModifyPermissionCompleteDTO>
{
    public ModifyPermissionUseCase(ModifyPermissionDto request)
        => (
            Request,
            ValidationManage,
            ErrorManage,
            IsValid,
            ValidationType
        ) = (
            request,
            new(EventUser.ModifyPermissionComplete, 1, typeof(ModifyPermissionCompleteDTO), typeof(ModifyPermissionException)),
            new(EventUser.ModifyPermissionComplete, 1, typeof(ModifyPermissionCompleteDTO), typeof(ModifyPermissionException)),
            () => Validators is not null && Validators.Any() ?
                Request.ThrowIfNull((errorMessage) => new GenericMediatException<ModifyPermissionDto>(errorMessage, request)).IfNotValid(Validators).Value is not null : true,
            typeof(IEnumerable<IValidator<ModifyPermissionDto>>)
        );
}