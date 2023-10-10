using FluentValidation;
using N5.User.Domain.Application;
using N5.Event.User;
using N5.User.Application.Exceptions.Comment;
using N5.User.Application.Exceptions.ValidatableExtension;
using N5.User.Domain.DTO;
using Throw;
using N5.User.Application.Exceptions;

namespace N5.User.Application.UseCases.Comment;

public class CreatePermissionUseCase : UserUseCase<CreatePermissionDto, CreatePermissionCompleteDTO>
{
    public CreatePermissionUseCase(CreatePermissionDto request)
        => (
            Request,
            ValidationManage,
            ErrorManage,
            IsValid,
            ValidationType
        ) = (
            request,
            new(EventUser.CreatePermissionComplete, 1, typeof(CreatePermissionCompleteDTO), typeof(CreatePermissionException)),
            new(EventUser.CreatePermissionComplete, 1, typeof(CreatePermissionCompleteDTO), typeof(CreatePermissionException)),
            () => Validators is not null && Validators.Any() ?
                Request.ThrowIfNull((errorMessage) => new GenericMediatException<CreatePermissionDto>(errorMessage, request)).IfNotValid(Validators).Value is not null : true,
            typeof(IEnumerable<IValidator<CreatePermissionDto>>)
        );
}