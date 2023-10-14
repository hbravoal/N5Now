using Bogus;
using FluentValidation.TestHelper;
using N5.User.Application.Rules.Permission;
using N5.User.Domain.DTO;
using N5.User.Test.Extend;

namespace N5.User.Test.Application.Rules.Permission;

public class ModifyCommentValidatorTest
{
    private ModifyPermissionValidator validator;

    [SetUp]
    public void Setup()
        => validator = new ModifyPermissionValidator();

    [Test]
    public void Success()
    {
        var model = new Faker<ModifyPermissionDto>().GenerateObject();
        var result = validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.EmployeeForename);
    }
}