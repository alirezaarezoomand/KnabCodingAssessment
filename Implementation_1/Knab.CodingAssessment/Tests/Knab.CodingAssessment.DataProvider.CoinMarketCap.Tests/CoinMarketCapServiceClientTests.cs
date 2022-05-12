using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Knab.CodingAssessment.DataProvider.CoinMarketCap.Tests
{
    public class CoinMarketCapServiceClientTests
    {
        [Fact]
        public async Task GetLatestQuotesAsync_WhenSymbolIsBTCUSD_ReturnLastQuote()
        {
            //Arrange
            var baseCurrency = "BTC";
            var quoteCurrency = "USD";
            var dateTime = DateTime.Now;
            var price = 30000.00M;

            var options = Options.Create(new CoinMarketCapOptions() { ApiKey = string.Empty, ApiUrl = "http://someapi.com/" });
            var loggerDummy = new Mock<ILogger<CoinMarketCapServiceClient>>();
            var httpClientMock = CreateHttpClientMockForBTCUSD(dateTime, price);

            //Act
            var sut = new CoinMarketCapServiceClient(httpClientMock, options, loggerDummy.Object);
            var result = await sut.GetLatestQuotesAsync(baseCurrency, quoteCurrency);

            //Assert
            Assert.Equal(baseCurrency, result.BaseCurrency);
            Assert.Equal(quoteCurrency, result.QuoteCurrency);
            Assert.Equal(dateTime, result.LastUpdate);
            Assert.Equal(price, result.Price);
        }

        private HttpClient CreateHttpClientMockForBTCUSD(DateTime dateTime, decimal price)
        {
            var httpMessageHandlerMock = new Mock<HttpMessageHandler>();

            var clientResponse = new
            {
                data = new
                {
                    BTC = new List<object>()
                    {
                        new
                        {
                            quote = new {
                                USD = new
                                {
                                    price = price,
                                    last_updated = dateTime,
                                }
                            },
                        }
                    }
                }
            };
            var clinetResponseString = JsonSerializer.Serialize(clientResponse);
            var response = new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(clinetResponseString),
            };

            //var httpClientMock = new Mock<HttpClient>();
            //httpClientMock.Setup(e => e.SendAsync(It.IsAny<HttpRequestMessage>()))
            //    .ReturnsAsync(response);

            httpMessageHandlerMock
              .Protected()
              .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
              .ReturnsAsync(response);

            var httpClientMock = new HttpClient(httpMessageHandlerMock.Object);

            return httpClientMock;
        }
    }
}