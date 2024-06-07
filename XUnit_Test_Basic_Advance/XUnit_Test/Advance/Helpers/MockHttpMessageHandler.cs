using Domain.Advance.Models;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;

namespace ProjectTest_XUnit.Advance.Helpers
{

    // In This Class we create a dummy sample of the http request
    public static class MockHttpMessageHandler<T>
    {
        public static Mock<HttpMessageHandler> SetupBasicGetResourceList(List<T> expectedResponce)
        {

            // We Get the List Of User and Put THem in the content
            var mockResponce = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expectedResponce))
            };

            // Then We Define the content type of responce
            mockResponce.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");


            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(mockResponce);
            return handlerMock;


        }
        public static Mock<HttpMessageHandler> SetupBasicGetResourceList(List<User> expectedResponce, string endpoint)
        {

            // We Get the List Of User and Put Them in the content
            var mockResponce = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(expectedResponce))
            };

            // Then We Define the content type of responce
            mockResponce.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");


            var handlerMock = new Mock<HttpMessageHandler>();



            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(req =>
                        req.Method == HttpMethod.Get &&
                        req.RequestUri == new Uri(endpoint)),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(mockResponce);
            return handlerMock;
        }


        internal static Mock<HttpMessageHandler> SetupReturn404()
        {
            // We Create a NotFound Responce with "" content
            var mockResponce = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
            {
                Content = new StringContent("")
            };

            // Then We Define the content type of responce
            mockResponce.Content.Headers.ContentType
                = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");


            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(mockResponce);
            return handlerMock;

        }


    }
}
