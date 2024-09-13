using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;

namespace FamilyHubs.SharedKernel.UnitTests.Identity.TestHelpers
{
    public static class MessageHandler
    {
        public static Mock<HttpMessageHandler> SetupMessageHandlerMock(HttpResponseMessage response, Uri baseUrl, HttpMethod httpMethod)
        {
            var httpMessageHandler = new Mock<HttpMessageHandler>();
            httpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.Is<HttpRequestMessage>(c => ValidateMessage(c,baseUrl,httpMethod)),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync((HttpRequestMessage request, CancellationToken token) => response);
            return httpMessageHandler;
        }

        private static bool ValidateMessage(HttpRequestMessage requestMessage, Uri baseUrl, HttpMethod httpMethod)
        {
            if (requestMessage.RequestUri == null) return false;
            if (requestMessage.RequestUri != baseUrl) return false;
            if (requestMessage.Method != httpMethod) return false;

            return true;
        }
    }
}
