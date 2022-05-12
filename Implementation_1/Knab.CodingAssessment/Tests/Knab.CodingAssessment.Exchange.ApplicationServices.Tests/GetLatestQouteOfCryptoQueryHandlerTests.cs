using Knab.CodingAssessment.Exchange.ApplicationServices.Contracts.Queries;
using Knab.CodingAssessment.Exchange.ApplicationServices.QueryHandlers;
using Knab.CodingAssessment.Exchange.Domain.Quotes;
using Knab.CodingAssessment.Exchange.Domain.Quotes.Exceptions;
using Knab.CodingAssessment.Exchange.Domain.Quotes.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Knab.CodingAssessment.Exchange.ApplicationServices.Tests
{
    public class GetLatestQuoteOfCryptoQueryHandlerTests
    {
        [Fact]
        public async Task HandleAsync_WhenSymbolIsBTCUSD_ReturnsCorrectResult()
        {
            //Arrange
            var symbol = new Symbol(Currency.CreateCrypto("btc"), Currency.CreateFiat("usd"));
            var dateTime = DateTime.Now;
            var quoteRepositoryMock = new Mock<IQuoteRepository>();
            var filter = new GetLatestQuoteOfCryptoQueryFilter()
            {
                BaseCurrency = "BTC",
                QuoteCurrency = "USD"
            };
            decimal price = 32000.11M;
            var quote = new Quote(symbol, dateTime, price);
            quoteRepositoryMock.Setup(r => r.GetLatestQuoteOf(It.IsAny<Symbol>()))
                .Returns(Task.FromResult(quote));

            //Act
            var sut = new GetLatestQuoteOfCryptoQueryHandler(quoteRepositoryMock.Object);
            var result = await sut.HandleAsync(filter);

            //Assert
            Assert.Equal(price, result.Price);
            Assert.Equal(dateTime, result.LastUpdate);
            Assert.Equal(symbol.Name, result.Symbol);

        }

        [Fact]
        public async Task HandleAsync_WhenSymbolIsInvalidUSD_ThrowInvalidCurrencyAbbreviationException()
        {
            //Arrange
            var quoteRepositoryMock = new Mock<IQuoteRepository>();
            var filter = new GetLatestQuoteOfCryptoQueryFilter()
            {
                BaseCurrency = "xx",
                QuoteCurrency = "USD"
            };
            quoteRepositoryMock.Setup(r => r.GetLatestQuoteOf(It.IsAny<Symbol>()));

            //Act
            var sut = new GetLatestQuoteOfCryptoQueryHandler(quoteRepositoryMock.Object);
            await Assert.ThrowsAsync<InvalidCurrencyAbbreviationException>(async () => await sut.HandleAsync(filter));

        }
    }
}