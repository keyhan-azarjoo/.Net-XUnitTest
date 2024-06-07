using Domain.Advance.Config;
using Domain.Advance.Models;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using ProjectTest_XUnit.Advance.Fixtures;
using ProjectTest_XUnit.Advance.Helpers;

// Link of Youtube toturial: https://www.youtube.com/watch?v=ULJ3UEezisw

namespace ProjectTest_XUnit.Advance.Systems.Services
{
    public class TestUserService
    {
        // In this class we test the behaviour of the Service 
        // First Of All as we don't have an actual httpclient, we create our own  httpclient
        // First we use some test data as the result
        // then we creeate the httpresponce in SetupBasicGetResourceList
        // All Of them as httpClient
        // then as we have the httpclient injected to the service, we inject the httpclient to the service
        // Then we call the Get AllUser and check the result
        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokesHttpGetRequest()
        {
            // Arrange
            // First We need to create a sample  of httpresponce

            var expectedResponce = UserFixture.GetTestUsers(); // We Grt The Users Here
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponce); // We Pass the users to create a httpresponce here
            
            var httpCLient = new HttpClient(handlerMock.Object); // Then we pass that created response to the httpclient.

            var config = Options.Create(new UserApiOptions
            {
                Endpoint = "https://www.example.com"
            });

            var sut = new UserService(httpCLient, config); // Then inject it to the user service


            // Action
            await sut.GetAllUsers(); // Here we call the userService 

            // Assert
            // Verify Http request is made

            handlerMock
                .Protected()
                .Verify("SendAsync",
                    Times.Exactly(1),
                    ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                    ItExpr.IsAny<CancellationToken>()
                );

        }


        // We Check the case that the result was 0
        [Fact]
        public async Task GetAllUsers_WhenHits404_ReturnsEmptyListOfUsers()
        {


            // Arrange
            // First We need to create a sample  of httpresponce

            var handlerMock = MockHttpMessageHandler<User>.SetupReturn404(); // We create a NotFountResponce

            var httpCLient = new HttpClient(handlerMock.Object); // Then we pass that created response to the httpclient.

            var config = Options.Create(new UserApiOptions
            {
                Endpoint = "https://www.example.com"
            });
            var sut = new UserService(httpCLient, config); // Then inject it to the user service


            // Action
            var result = await sut.GetAllUsers(); // Here we call the userService 

            // Assert
            result.Count.Should().Be(0);
        }







        // We Check the case that the result was 0
        [Fact]
        public async Task GetAllUsers_WhenCalled_ReturnsListOfUsersOfExpectedSize()
        {


            // Arrange
            // First We need to create a sample  of httpresponce
            var expectedResponce = UserFixture.GetTestUsers(); // We Grt The Users Here
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponce); // We Pass the users to create a httpresponce here

            var httpCLient = new HttpClient(handlerMock.Object); // Then we pass that created response to the httpclient.

            var config = Options.Create(new UserApiOptions
            {
                Endpoint = "https://www.example.com"
            });
            var sut = new UserService(httpCLient, config); // Then inject it to the user service


            // Action
            var result = await sut.GetAllUsers(); // Here we call the userService 

            // Assert
            result.Count.Should().Be(expectedResponce.Count);
        }









        // We Check the case that the behavour of an external API
        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokesConfiguredExternalUrl()
        {

            // Arrange
            // First We need to create a sample  of httpresponce
            var expectedResponce = UserFixture.GetTestUsers(); // We Grt The Users Here


            var endpoint = "https://www.example.com/Users"; 
            var handlerMock = MockHttpMessageHandler<User>
                .SetupBasicGetResourceList(expectedResponce, endpoint); // We Pass the users to create a httpresponce here from the Endpoint

            var httpCLient = new HttpClient(handlerMock.Object); // Then we pass that created response to the httpclient.

            var config = Options.Create(
                new UserApiOptions
                {
                    Endpoint = endpoint
                });

            var sut = new UserService(httpCLient, config); // Then inject it to the user service


            // Action
            var result = await sut.GetAllUsers(); // Here we call the userService 

            var uri = new Uri(endpoint);

            // Assert
            handlerMock
                .Protected()
                .Verify("SendAsync",
                    Times.Exactly(1),
                    ItExpr.Is<HttpRequestMessage>(
                        req => req.Method == HttpMethod.Get 
                               && req.RequestUri == uri),
                    ItExpr.IsAny<CancellationToken>()
                );


        }
    }
}
