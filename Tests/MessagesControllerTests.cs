using ExampleProjectAPI.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Model.Entities;
using Moq;
using Service.Abstract;
using Service.DtoModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests
{
    public class MessagesControllerTests
    {
        private readonly Mock<IMessageService> serviceStub = new Mock<IMessageService>();
        private readonly Mock<UserManager<ApplicationUser>> userManagerStub = UserManagerMock.MockUserManager(UserManagerMock.SampleUserList);

        public MessageOutDto CreateMessageOut(string senderId = "a", string receiverId = "b")
        {
            var message = new MessageOutDto() { Id = 1, ReceiverId = receiverId, ReceiverName = "User", SenderId = senderId, SenderName = "User2", Topic = "topic", Content = "some content", DateSend = DateTime.Now };
            return message;
        }
        [Fact]
        public async Task GetMessageAsync_WithNotExistingMessage_ReturnsNotFound()
        {
            //Arrange
            serviceStub.Setup(service => service.GetMessageAsync(It.IsAny<int>()))
                .ReturnsAsync((MessageOutDto)null);

            var controller = new MessagesController(serviceStub.Object, userManagerStub.Object);

            //Act
            var result = await controller.GetMessageAsync(default);

            //Assert
            Assert.IsType<NotFoundResult>(result);
        }
        [Fact]
        public async Task GetMessageAsync_WithExistingMessage_UserMessage_ReturnsMessageOutDto()
        {
            //Arrange
            var message = CreateMessageOut();
            serviceStub.Setup(service => service.GetMessageAsync(It.IsAny<int>()))
                .ReturnsAsync(message);

            userManagerStub.Setup(manager => manager.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(UserManagerMock.SampleUserList.First());

            var controller = new MessagesController(serviceStub.Object, userManagerStub.Object);
            controller.SetupIdentity();

            //Act
            var result = await controller.GetMessageAsync(1);

            //Assert
            Assert.IsType<OkObjectResult>(result);
            var okResult = (result as OkObjectResult);
            okResult.Value.Should().BeEquivalentTo<MessageOutDto>(message, options => options.ComparingByMembers<MessageOutDto>());
        }
        [Fact]
        public async Task GetMessageAsync_WithExistingMessage_NotUserMessage_ReturnsMessageOutDto() {

            //Arrange
            var message = CreateMessageOut("c", "d");
            serviceStub.Setup(service => service.GetMessageAsync(It.IsAny<int>()))
                .ReturnsAsync(message);

            userManagerStub.Setup(manager => manager.FindByNameAsync(It.IsAny<string>()))
                .ReturnsAsync(UserManagerMock.SampleUserList.First());

            var controller = new MessagesController(serviceStub.Object, userManagerStub.Object);
            controller.SetupIdentity("a");

            //Act
            var result = await controller.GetMessageAsync(1);

            //Assert
            Assert.IsType<UnauthorizedResult>(result);
        }
    }
}
