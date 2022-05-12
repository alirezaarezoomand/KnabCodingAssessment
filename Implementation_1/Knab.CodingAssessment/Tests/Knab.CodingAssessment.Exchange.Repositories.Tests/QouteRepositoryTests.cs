using Knab.CodingAssessment.Exchage.DataProvider;
using Knab.CodingAssessment.Exchange.Domain.Quotes;
using Knab.CodingAssessment.Exchange.Repositories.Quotes;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Knab.CodingAssessment.Exchange.Repositories.Tests
{
    public class QuoteRepositoryTests
    {
        [Fact]
        public async Task GetLatestQuoteOf_WhenSymbolIsEURUSD_ReturnsCorrectResult()
        {
            //Arrange
            var symbol = new Symbol(Currency.CreateCrypto("btc"), Currency.CreateFiat("usd"));
            var dateTime = DateTime.Now;
            var exhcangeDataProviderMock = new Mock<IExchangeDataProvider>();
            decimal price = 32000.11M;
            var quotesResponse = new QuotesResponse()
            {
                BaseCurrency = symbol.BaseCurrency.Abbreviation,
                QuoteCurrency = symbol.QuoteCurrency.Abbreviation,
                LastUpdate = dateTime,
                Price = price,
            };

            exhcangeDataProviderMock.Setup(e => e.GetLatestQuotesAsync(symbol.BaseCurrency.Abbreviation, symbol.QuoteCurrency.Abbreviation))
                .Returns(Task.FromResult(quotesResponse));

            //Act
            var sut = new QuoteRepository(exhcangeDataProviderMock.Object);
            var result = await sut.GetLatestQuoteOf(symbol);

            //Assert
            Assert.Equal(price, result.Price);
            Assert.Equal(dateTime, result.Date);
            Assert.Equal(symbol.Name, result.Symbol.Name);

        }
    }
}