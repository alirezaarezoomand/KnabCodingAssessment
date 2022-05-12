using Castle.Core.Configuration;
using Knab.CodingAssessment.Exchange.ApplicationServices.Contracts.Queries;
using Knab.CodingAssessment.Exchange.Endpoint.WebApi.Controllers;
using Knab.CodingAssessment.Seedwork.Queries;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Knab.CodingAssessment.Exchange.Endpoint.WebApi.Tests
{
    public class QuoteControllerTests
    {
        [Theory]
        [InlineData("BTC")]
        public async Task Get_ReturnsFiveItems(string cryptoAbbreviation)
        {
            //Arrenge
            var quoteCurrencies = new List<string>() { "USD", "EUR", "BRL", "GBP", "AUD" };
            var dateTime = DateTime.Now;
            var quotes = quoteCurrencies.Select(s => new LatestQuoteOfCryptoQueryResult() { Symbol = cryptoAbbreviation + s, LastUpdate = dateTime, Price = 0 });

            var loggerDummy = new Mock<ILogger<QuoteController>>();
            var queryBusMock = new Mock<IQueryBus>();
            queryBusMock.Setup(e => e.Dispatch<GetLatestQuoteOfCryptoQueryFilter, LatestQuoteOfCryptoQueryResult>(It.IsAny<GetLatestQuoteOfCryptoQueryFilter>()))
                .Returns(Task.FromResult(quotes.FirstOrDefault(q=>q.Symbol.StartsWith(cryptoAbbreviation))));

            var appSettings = JsonConvert.SerializeObject(new
            {
                QuoteCurrencies = quoteCurrencies
            });
            var builder = new ConfigurationBuilder();
            builder.AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(appSettings)));
            var configurationMock = builder.Build();

            //Act
            var sut = new QuoteController(loggerDummy.Object, queryBusMock.Object, configurationMock);
            var result = await sut.Get(cryptoAbbreviation);

            //Assert
            Assert.True(result.Succeeded);
            Assert.Equal(quotes.Count(), result.Result.TotalItems);
        }

        
    }
}