using Knab.CodingAssessment.Exchage.DataProvider;
using Knab.CodingAssessment.Exchange.Domain.Quotes;
using Knab.CodingAssessment.Exchange.Domain.Quotes.Services;

namespace Knab.CodingAssessment.Exchange.Repositories.Quotes
{
    public class QuoteRepository : IQuoteRepository
    {
        private readonly IExchangeDataProvider _exchangeDataProvider;

        public QuoteRepository(IExchangeDataProvider exchangeDataProvider)
        {
            _exchangeDataProvider = exchangeDataProvider;
        }

        public async Task<Quote> GetLatestQuoteOf(Symbol symbol)
        {
            
            var response = await _exchangeDataProvider.GetLatestQuotesAsync(symbol.BaseCurrency.Abbreviation, symbol.QuoteCurrency.Abbreviation);
            if (response == null)
            {
                return null;
            }

            var quote = new Quote(symbol, response.LastUpdate, response.Price);
            return quote;
        }
    }
}
