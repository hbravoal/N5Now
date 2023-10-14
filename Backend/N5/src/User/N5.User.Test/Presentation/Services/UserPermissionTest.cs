using Bogus;
using MediatR;
using Moq;
using N5.Eda.Interfaces;
using N5.Event.User;
using N5.TryCatch.Extend;
using N5.User.Domain.DTO;
using N5.User.Services.Handlers.UserPermission;
using N5.User.Test.Extend;
using N5.Utilities;

namespace N5.User.Test.Presentation.Services;

[TestFixture]
public class UserPermissionTest
{

    [Test]
    public void CreateCommentTest()
        => this.Try(() =>
        {
            //Arrange
            var request = new Faker<CreatePermissionDto>().GenerateObject();
            CancellationToken cancelationToken = CancellationToken.None;

            //Mock
            Mock<IBrokerPayload> mockPayload = new();
            mockPayload.SetupGet(x => x.Topic).Returns(EventUser.CreatePermission);
            mockPayload.SetupGet(x => x.Value).Returns(request.ToSerializeJSON());
            Mock<IBrokerConfiguration> mockConfiguration = new();
            Mock<IMediator> mockMediator = new();

            //Action
            var service = new CreateUserPermission(mockConfiguration.Object, mockMediator.Object);
            service.Handler(mockPayload.Object, cancelationToken).GetAwaiter().GetResult();

            return;
        })
        .Catch((ex) => Assert.Fail(ex.Message))
        .Apply();

  
    [Test]
    public void UpdateCommentTest()
        => this.Try(() =>
        {
            //Arrange
            var request = new Faker<ModifyPermissionDto>().GenerateObject();
            CancellationToken cancelationToken = CancellationToken.None;

            //Mock
            Mock<IBrokerPayload> mockPayload = new();
            mockPayload.SetupGet(x => x.Topic).Returns(EventUser.ModifyPermission);
            mockPayload.SetupGet(x => x.Value).Returns(request.ToSerializeJSON());
            Mock<IBrokerConfiguration> mockConfiguration = new();
            Mock<IMediator> mockMediator = new();

            //Action
            var service = new ModifyUserPermission(mockConfiguration.Object, mockMediator.Object);
            service.Handler(mockPayload.Object, cancelationToken).GetAwaiter().GetResult();

            return;
        })
        .Catch((ex) => Assert.Fail(ex.Message))
        .Apply();


    [Test]
    public void GetCommentsTest()
        => this.Try(() =>
        {
            //Arrange
            var request = new Faker<GetPermissionDto>().GenerateObject();
            CancellationToken cancelationToken = CancellationToken.None;

            //Mock
            Mock<IBrokerPayload> mockPayload = new();
            mockPayload.SetupGet(x => x.Topic).Returns(EventUser.GetPermission);
            mockPayload.SetupGet(x => x.Value).Returns(request.ToSerializeJSON());
            Mock<IBrokerConfiguration> mockConfiguration = new();
            Mock<IMediator> mockMediator = new();

            //Action
            var service = new GetUserPermission(mockConfiguration.Object, mockMediator.Object);
            service.Handler(mockPayload.Object, cancelationToken).GetAwaiter().GetResult();

            return;
        })
        .Catch((ex) => Assert.Fail(ex.Message))
        .Apply();

}