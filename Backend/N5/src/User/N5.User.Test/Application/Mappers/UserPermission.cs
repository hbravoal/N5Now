using AutoMapper;
using Bogus;
using N5.User.Application.Configurations.Mappers;
using N5.User.Domain.DTO;
using N5.User.Domain.Entities;
using N5.User.Test.Extend;

namespace N5.User.Test.Application.Mappers;

[TestFixture]
public class UserPermissionTest
{
    private MapperConfiguration config;

    [SetUp]
    public void Setup()
        => config = new MapperConfiguration(cfg => cfg.AddProfile<UserPermissionMapper>());

    [Test]
    public void AutoMappingCreateCommentValid()
    {
        //Arrange
        var mapper = config.CreateMapper();

        //mock
        var request = new Faker<CreatePermissionDto>().GenerateObject();

        //Action
        var entity = mapper.Map<UserPermission>(request);

        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(entity.EmployeeForename, Is.EqualTo(request.EmployeeForename));
            Assert.That(entity.EmployeeSurname, Is.EqualTo(request.EmployeeSurname));
            Assert.That(entity.PermissionTypeId, Is.EqualTo(request.PermissionTypeId));
        });
    }

    [Test]
    public void AutoMappingGetCommentsValid()
    {
        //Arrange
        var mapper = config.CreateMapper();

        //mock
        var commentEntity = new Faker<UserPermission>().GenerateObject();

        //Action
        var commentResult = mapper.Map<CreatePermissionDto>(commentEntity);

        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(commentResult, Is.Not.Null);
            Assert.That(commentResult.EmployeeForename, Is.EqualTo(commentEntity.EmployeeForename));
            Assert.That(commentResult.EmployeeSurname, Is.EqualTo(commentEntity.EmployeeSurname));
            Assert.That(commentResult.PermissionTypeId, Is.EqualTo(commentEntity.PermissionTypeId));
        });
    }
}