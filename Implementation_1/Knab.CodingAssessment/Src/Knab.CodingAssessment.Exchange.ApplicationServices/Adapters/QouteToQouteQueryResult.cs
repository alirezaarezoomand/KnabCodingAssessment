using Knab.CodingAssessment.Exchange.ApplicationServices.Contracts.Queries;
using Knab.CodingAssessment.Exchange.Domain.Quotes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knab.CodingAssessment.Exchange.ApplicationServices.Adapters
{
    internal static class QuoteToQuoteQueryResult
    {
        public static LatestQuoteOfCryptoQueryResult? ToLatestQuoteOfCryptoQueryResult(this Quote quote)
        {
            if (quote == null) return null;
            var result = new LatestQuoteOfCryptoQueryResult()
            {
                Symbol = quote.Symbol.Name,
                LastUpdate = quote.Date,
                Price = quote.Price
            };

            return result;
        }
    }
}
