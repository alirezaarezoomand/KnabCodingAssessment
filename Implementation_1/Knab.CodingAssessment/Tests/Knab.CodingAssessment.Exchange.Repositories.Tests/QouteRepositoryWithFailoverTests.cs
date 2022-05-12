using Knab.CodingAssessment.Exchage.DataProvider;
using Knab.CodingAssessment.Exchange.Domain.Quotes;
using Knab.CodingAssessment.Exchange.Domain.Quotes.Services;
using Knab.CodingAssessment.Exchange.Repositories.Quotes;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Knab.CodingAssessment.Exchange.Repositories.Tests
{
    public class QuoteRepositoryWithFailoverTests
    {
        [Fact]
        public async Task GetLatestQuoteOf_WhenSymbolIsEURUSD_ReturnsCorrectResultFromCache()
        {
            //Arrange
            var symbol = new Symbol(Currency.CreateCrypto("btc"), Currency.CreateFiat("usd"));
            var dateTime = DateTime.Now;
            var quoteRepositoryMock = new Mock<IQuoteRepository>();
            decimal price = 32000.11M;

            quoteRepositoryMock.Setup(r => r.GetLatestQuoteOf(It.IsAny<Symbol>()))
                .ReturnsAsync(default(Quote));

            var chachedQuote = new Quote(symbol, dateTime, price);

            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            memoryCache.Set(symbol.Name, chachedQuote);

            //Act
            var sut = new QuoteRepositoryWithFailover(quoteRepositoryMock.Object, memoryCache);
            var result = await sut.GetLatestQuoteOf(symbol);

            //Assert
            Assert.Equal(price, result.Price);
            Assert.Equal(dateTime, result.Date);
            Assert.Equal(symbol.Name, result.Symbol.Name);

        }

        [Fact]
        public async Task GetLatestQuoteOf_WhenSymbolIsEURUSD_StoreResultInChache()
        {
            //Arrange
            var symbol = new Symbol(Currency.CreateCrypto("btc"), Currency.CreateFiat("usd"));
            var dateTime = DateTime.Now;
            var quoteRepositoryMock = new Mock<IQuoteRepository>();
            decimal price = 32000.11M;
            var quote = new Quote(symbol, dateTime, price);

            quoteRepositoryMock.Setup(r => r.GetLatestQuoteOf(It.IsAny<Symbol>()))
                .Returns(Task.FromResult(quote));

            var memoryCache = new MemoryCache(new MemoryCacheOptions());

            //Act
            var sut = new QuoteRepositoryWithFailover(quoteRepositoryMock.Object, memoryCache);
            await sut.GetLatestQuoteOf(symbol);
            var chachedQuote = memoryCache.Get<Quote>(symbol.Name);

            //Assert
            Assert.Equal(price, chachedQuote.Price);
            Assert.Equal(dateTime, chachedQuote.Date);
            Assert.Equal(symbol.Name, chachedQuote.Symbol.Name);

        }
    }
}