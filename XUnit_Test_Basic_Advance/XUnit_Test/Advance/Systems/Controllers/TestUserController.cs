using Domain.Advance.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ProjectTest_XUnit.Advance.Fixtures;
using Web.Controllers;

// Link of Youtube toturial: https://www.youtube.com/watch?v=ULJ3UEezisw

namespace ProjectTest_XUnit.Advance.Systems.Controllers
{
    public class TestUserController
    {

        // In this Unit Test We chack the Status Code For The API EndPoint
        // In addition to that, we inject a dependency injection to the controller

        // In this Teat We Used Mock Which is a function from Moq library and create a dommy class of your dependency injection and you can pass it to your controller in this case.



        [Fact]
        public async Task Get_OneSuccess_ReturnStatusCode200()
        {
            // Arrange
            // Instal Moq from NugetPackegeManager
            var mokUsersService = new Mock<IUserService>();

            mokUsersService
                .Setup(service => service.GetAllUsers())
                .ReturnsAsync(UserFixture.GetTestUsers); // Here We create a list of users in the Fixture class and use those test users to pass by the injection stuf

            var sut = new CalculateController(mokUsersService.Object);


            // Act
            var result =(OkObjectResult) await sut.Get();


            // Assert
            result.StatusCode.Should().Be(200);

        }







        // In this test We check that has a function been called in the controller or no and hoemany Time it's been called
        // We inject the dependency injection and check the number of GetAllUsers invoke
        [Fact]
        public async Task Get_OnSuccess_InvokesUserServiceExactlyOnce()
        {
            // Arrange
            var mokUsersService = new Mock<IUserService>();

            mokUsersService
                .Setup(service => service.GetAllUsers())
                .ReturnsAsync(new List<User>());

            var sut = new CalculateController(mokUsersService.Object);
            // Act
            var result = await sut.Get();

            // Assert
            mokUsersService.Verify(service => service.GetAllUsers(),
                Times.Once);

        }







        // Here we check the result of the Controller and it should be a list of Users
        [Fact]
        public async Task Get_OnSuccess_ReturnListOfUsers()
        {
            // Arrange
            var mokUsersService = new Mock<IUserService>();

            mokUsersService
                .Setup(service => service.GetAllUsers())
                .ReturnsAsync(UserFixture.GetTestUsers);

            var sut = new CalculateController(mokUsersService.Object);
            // Act


            var result = await sut.Get();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var objectResult = (OkObjectResult)result;
            objectResult.Value.Should().BeOfType<List<User>>();

        }







        // Here We check that is it return a nutFount Exception when there is no usere?
        [Fact]
        public async Task Get_OnNoUserFound_Returns404()
        {

            var mokUsersService = new Mock<IUserService>();

            mokUsersService
                .Setup(service => service.GetAllUsers())
                .ReturnsAsync(new List<User>());

            var sut = new CalculateController(mokUsersService.Object);
            // Act


            var result = await sut.Get();

            // Assert
            result.Should().BeOfType<NotFoundResult>();
            var objectResult = (NotFoundResult) result;
            objectResult.StatusCode.Should().Be(404);
        }

    }
}
