using Bogus;
using N5.User.Domain.Entities;
using N5.User.Test.Extend;

namespace N5.User.Test.Infrastructure.Persistence;

[TestFixture]
public class UserPermissionRepository
{

    [Test]
    public void CreateUserPermissionSuccessCase()
    {
        //Arrange

        var commentEntity = new Faker<UserPermission>().GenerateObject();

        //Mock
        var unitOfWork = UserContextExtend.UnitOfWork;
        unitOfWork.UserPermissionRepository.AddAsync(commentEntity).Wait();
        unitOfWork.SaveAsync().GetAwaiter().GetResult();

        //Assert
        Assert.That(commentEntity.Id, Is.GreaterThan(0));
    }


    [Test]
    public void UpdateUserPermissionSuccessCase()
    {
        //Arrange

        var request = new Faker<UserPermission>().GenerateObject();

        //Action
        var unitOfWork = UserContextExtend.UnitOfWork;
        var commentToUpdate = unitOfWork.UserPermissionRepository.GetFirstOrDefaultAsync(withDisabled: true).Result;
        commentToUpdate.EmployeeForename = request.EmployeeForename;
        commentToUpdate.LastUpdate = DateTime.UtcNow;
        unitOfWork.UserPermissionRepository.UpdateAsync(commentToUpdate, CancellationToken.None).GetAwaiter().GetResult();
        unitOfWork.SaveAsync().Wait();

        var commentEntity = unitOfWork.UserPermissionRepository.GetByIdAsync(commentToUpdate.Id, true, CancellationToken.None).GetAwaiter().GetResult();

        //Assert
        Assert.That(commentEntity, Is.Not.Null);
        Assert.That(commentEntity.EmployeeForename, Is.EqualTo(request.EmployeeForename));
        Assert.That(commentEntity.LastUpdate, Is.EqualTo(commentToUpdate.LastUpdate));
    }

    [Test]
    public void GetEnabledPermissionSuccessCase()
    {
        //Arrage
        var unitOfWork = UserContextExtend.UnitOfWork;
        var comment = unitOfWork.UserPermissionRepository.GetFirstOrDefaultAsync().Result;

        //Action
        var response = unitOfWork.UserPermissionRepository.GetAsync(f => f.Id == comment.Id && f.Enabled == true).GetAwaiter().GetResult();

        //Assert
        Assert.That(response, Is.Not.Null);
        Assert.That(response.Count, Is.GreaterThan(0));
    }


}