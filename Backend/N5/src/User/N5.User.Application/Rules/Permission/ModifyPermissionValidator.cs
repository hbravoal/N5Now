﻿using FluentValidation;
using N5.User.Domain.DTO;

namespace N5.User.Application.Rules.Permission;

public class ModifyPermissionValidator : AbstractValidator<ModifyPermissionDto>
{
    public ModifyPermissionValidator()
    {
        RuleFor(x => x.IdSession).NotNull().WithMessage(x => "IdSession is required");
//        RuleFor(x => x.IdSession).NotNull().Must(x => SpecialValidation.BeValidGuid(x)).WithMessage(x => "IdSession is required");
        /* RuleFor(x => x.EmployeeForename).NotNull().WithMessage(x => "Employee forename is required");
         RuleFor(x => x.EmployeeSurname).NotNull().WithMessage(x => "Employee Surname is required");
         RuleFor(x => x.PermissionTypeId).NotNull().Must(x => x>0).WithMessage(x => "Permission is required");
         RuleFor(x => x.PermissionDate).NotNull().Must(x => x> DateTime.MinValue).WithMessage(x => "Permission date is required");*/
    }
}