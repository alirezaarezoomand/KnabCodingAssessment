using Knab.CodingAssessment.Exchange.ApplicationServices.Adapters;
using Knab.CodingAssessment.Exchange.ApplicationServices.Contracts.Queries;
using Knab.CodingAssessment.Seedwork.Queries;
using Knab.CodingAssessment.Exchange.Domain.Quotes;
using Knab.CodingAssessment.Exchange.Domain.Quotes.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knab.CodingAssessment.Exchange.ApplicationServices.QueryHandlers
{
    public class GetLatestQuoteOfCryptoQueryHandler :
        IQueryHandler<GetLatestQuoteOfCryptoQueryFilter, LatestQuoteOfCryptoQueryResult>
    {
        private readonly IQuoteRepository _quoteRepository;

        public GetLatestQuoteOfCryptoQueryHandler(IQuoteRepository quoteRepository)
        {
            _quoteRepository = quoteRepository;
        }

        public async Task<LatestQuoteOfCryptoQueryResult> HandleAsync(GetLatestQuoteOfCryptoQueryFilter filter)
        {
            var cryptoCurrency = Currency.CreateCrypto(filter.BaseCurrency);
            var fiatCurrency = Currency.CreateFiat(filter.QuoteCurrency);
            var symbol = new Symbol(cryptoCurrency, fiatCurrency);
            var quote = await _quoteRepository.GetLatestQuoteOf(symbol);
            if(quote == null) return null;
            return quote.ToLatestQuoteOfCryptoQueryResult();
        }
    }
}
