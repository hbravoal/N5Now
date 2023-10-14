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
    /// <summary>
    /// G: El usuario escribe un comentario
    /// W: El usuario hace clic en el botón save comment
    /// T: El usuario recibirá el comment item Id del comentario creado
    /// </summary>
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

    /// <summary>
    /// G: El usuario escribe un comentario
    /// W: El usuario hace clic en el botón save comment y se produce un error general al crearlo
    /// T: El usuario recibirá un error
    /// </summary>
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

    /// <summary>
    /// G: El usuario escribe un comentario
    /// W: El usuario hace clic en el botón save comment y se carga de forma incorrecta el request
    /// T: El usuario recibirá un error indicando un BadRequest
    /// </summary>
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

    /// <summary>
    /// G: El usuario actualiza un comentario
    /// W: El usuario hace clic en el botón save comment
    /// T: El usuario recibirá el comment item Id del comentario actualizado
    /// </summary>
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

    /// <summary>
    /// G: El usuario actualiza un comentario
    /// W: El usuario hace clic en el botón save comment y se carga de forma incorrecta el request
    /// T: El usuario recibirá un error indicando un BadRequest
    /// </summary>
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

    /// <summary>
    /// G: El usuario ingresa en una trasaccion
    /// W: El usuario solicita la lista de comentarios de la transaccion
    /// T: El usuario recibe la lista de comentarios relacionados a la transaccion
    /// </summary>
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

    /// <summary>
    /// G: El usuario ingresa en una trasaccion
    /// W: El usuario solicita la lista de comentarios de la transaccion y se carga de forma incorrecta el request
    /// T: El usuario recibirá un error indicando un BadRequest
    /// </summary>
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