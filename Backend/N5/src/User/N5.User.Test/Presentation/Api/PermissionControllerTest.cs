using Bogus;
using Microsoft.AspNetCore.Mvc;
using Moq;
using N5.Eda.RequestReply.Interface;
using N5.User.Api.Controllers;
using N5.User.Domain.DTO;
using N5.User.Test.Extend;
using System.Net;

namespace N5.User.Test.Presentation.Api;

[TestFixture]
public class PermissionControllerTest
{
  
    [Test]
    public void CreateDocumentControllerTest()
    {
        //Arrange
        var request = new Faker<CreatePermissionDto>().GenerateObject();
        var response = new Faker<CreatePermissionCompleteDTO>().GenerateObject();

        //Mock
        Mock<IRequestReplyExecute> mockRequestReply = new();
        mockRequestReply.Setup(a => a.Wait<CreatePermissionCompleteDTO>(It.IsAny<CreatePermissionDto>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TimeSpan>()))
            .Returns(Task.FromResult(response));

        //Action
        var controller = new PermissionController(mockRequestReply.Object);
        CreatePermissionCompleteDTO? responseController = (CreatePermissionCompleteDTO?)((OkObjectResult)controller.Create(request).GetAwaiter().GetResult()).Value;

        //Assert
        Assert.That(responseController.Id, Is.Positive);
    }

  
    [Test]
    public void CallFailedControllerTest()
    {
        //Arrange
        var request = new Faker<CreatePermissionDto>().GenerateObject();
        var response = new Faker<CreatePermissionCompleteDTO>().GenerateObject();

        //Mock
        Mock<IRequestReplyExecute> mockRequestReply = new();
        mockRequestReply.Setup(a => a.Wait<CreatePermissionCompleteDTO>(It.IsAny<CreatePermissionDto>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TimeSpan>()))
            .Throws(new Exception());

        //Action
        var controller = new PermissionController(mockRequestReply.Object);
        var responseController = (ObjectResult)controller.Create(request).GetAwaiter().GetResult();

        //Assert
        Assert.That(responseController.StatusCode, Is.EqualTo((int)HttpStatusCode.InternalServerError));
    }

   
    [Test]
    public void CreateCommentFailedModelStateControllerTest()
    {
        //Arrange
        var request = new Faker<CreatePermissionDto>().GenerateObject();
        var response = new Faker<CreatePermissionCompleteDTO>().GenerateObject();

        //Mock
        Mock<IRequestReplyExecute> mockRequestReply = new();
        mockRequestReply.Setup(a => a.Wait<CreatePermissionCompleteDTO>(It.IsAny<CreatePermissionDto>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TimeSpan>()))
            .Throws(new Exception());

        //Action
        var controller = new PermissionController(mockRequestReply.Object);
        controller.ModelState.AddModelError("IdSession", "Null");
        var responseController = (BadRequestResult)controller.Create(request).GetAwaiter().GetResult();

        //Assert
        Assert.That(responseController.StatusCode, Is.EqualTo((int)HttpStatusCode.BadRequest));
    }


    [Test]
    public void UpdateCommentControllerTest()
    {
        //Arrange
        var request = new Faker<ModifyPermissionDto>().GenerateObject();
        var response = new Faker<ModifyPermissionCompleteDTO>().GenerateObject(request.EmployeeForename);

        //Mock
        Mock<IRequestReplyExecute> mockRequestReply = new();
        mockRequestReply.Setup(a => a.Wait<ModifyPermissionCompleteDTO>(It.IsAny<ModifyPermissionDto>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TimeSpan>()))
            .Returns(Task.FromResult(response));

        //Action
        var controller = new PermissionController(mockRequestReply.Object);
        ModifyPermissionCompleteDTO? responseController = (ModifyPermissionCompleteDTO?)((OkObjectResult)controller.Modify(request).GetAwaiter().GetResult()).Value;

        //Assert
        Assert.That(responseController.EmployeeForename, Is.Not.Null);
    }


    [Test]
    public void UpdateCommentFailedModelStateControllerTest()
    {
        //Arrange
        var request = new Faker<ModifyPermissionDto>().GenerateObject();
        var response = new Faker<ModifyPermissionCompleteDTO>().GenerateObject();

        //Mock
        Mock<IRequestReplyExecute> mockRequestReply = new();
        mockRequestReply.Setup(a => a.Wait<ModifyPermissionCompleteDTO>(It.IsAny<ModifyPermissionDto>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TimeSpan>()))
            .Throws(new Exception());

        //Action
        var controller = new PermissionController(mockRequestReply.Object);
        controller.ModelState.AddModelError("IdSession", "Null");
        var responseController = (BadRequestResult)controller.Modify(request).GetAwaiter().GetResult();

        //Assert
        Assert.That(responseController.StatusCode, Is.EqualTo((int)HttpStatusCode.BadRequest));
    }

    [Test]
    public void GetCommentsControllerTest()
    {
        //Arrange
        var request = new Faker<GetPermissionDto>().GenerateObject();
        var response = new Faker<GetPermissionCompleteDTO>().GenerateObject();

        //Mock
        Mock<IRequestReplyExecute> mockRequestReply = new();
        mockRequestReply.Setup(a => a.Wait<GetPermissionCompleteDTO>(It.IsAny<GetPermissionDto>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TimeSpan>()))
            .Returns(Task.FromResult(response));

        //Action
        var controller = new PermissionController(mockRequestReply.Object);
        GetPermissionCompleteDTO? responseController = (GetPermissionCompleteDTO?)((OkObjectResult)controller.Get(request).GetAwaiter().GetResult()).Value;

        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(responseController.Permissions, Is.Not.Null);
        });
    }


    [Test]
    public void GetCommentsFailedModelStateControllerTest()
    {
        //Arrange
        var request = new Faker<GetPermissionDto>().GenerateObject();
        var response = new Faker<GetPermissionCompleteDTO>().GenerateObject();

        //Mock
        Mock<IRequestReplyExecute> mockRequestReply = new();
        mockRequestReply.Setup(a => a.Wait<GetPermissionCompleteDTO>(It.IsAny<GetPermissionDto>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<TimeSpan>()))
                .Throws(new Exception());

        //Action
        var controller = new PermissionController(mockRequestReply.Object);
        controller.ModelState.AddModelError("IdSession", "Null");
        var responseController = (BadRequestResult)controller.Get(request).GetAwaiter().GetResult();

        //Assert
        Assert.That(responseController.StatusCode, Is.EqualTo((int)HttpStatusCode.BadRequest));
    }


}