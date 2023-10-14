using Bogus;
using FluentValidation.TestHelper;
using N5.User.Application.Rules.Permission;
using N5.User.Domain.DTO;
using N5.User.Test.Extend;

namespace N5.User.Test.Application.Rules.Permission;

public class CreateCommentValidatorTest
{
    private CreatePermissionValidator validator;

    [SetUp]
    public void Setup()
        => validator = new CreatePermissionValidator();

    [Test]
    public void Success()
    {
        var model = new Faker<CreatePermissionDto>().GenerateObject();
        var result = validator.TestValidate(model);
        result.ShouldNotHaveValidationErrorFor(x => x.EmployeeForename);
    }
}