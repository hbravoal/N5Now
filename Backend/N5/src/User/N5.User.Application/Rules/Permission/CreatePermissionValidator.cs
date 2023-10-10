using FluentValidation;
using N5.User.Application.Rules.Common;
using N5.User.Domain.DTO;

namespace N5.User.Application.Rules.Comment;

public class CreatePermissionValidator : AbstractValidator<CreatePermissionDto>
{
    public CreatePermissionValidator()
    {
        RuleFor(x => x.IdSession).NotNull().Must(x => SpecialValidation.BeValidGuid(x)).WithMessage(x => "IdSession is required");

    }
}