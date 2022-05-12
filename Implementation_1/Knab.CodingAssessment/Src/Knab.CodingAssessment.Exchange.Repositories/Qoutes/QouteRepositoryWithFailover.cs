using Knab.CodingAssessment.Exchange.Domain.Quotes;
using Knab.CodingAssessment.Exchange.Domain.Quotes.Services;
using Microsoft.Extensions.Caching.Memory;

namespace Knab.CodingAssessment.Exchange.Repositories.Quotes
{
    public class QuoteRepositoryWithFailover : IQuoteRepository
    {
        private readonly IQuoteRepository _quoteRepository;
        private readonly IMemoryCache _cache;

        public QuoteRepositoryWithFailover(IQuoteRepository quoteRepository, IMemoryCache cache)
        {
            _quoteRepository = quoteRepository;
            _cache = cache;
        }

        public async Task<Quote> GetLatestQuoteOf(Symbol symbol)
        {

            var result = await _quoteRepository.GetLatestQuoteOf(symbol);

            if(result == null)
            {
                if(_cache.TryGetValue<Quote>(symbol.Name, out result))
                {
                    return result;
                }
                return null;
            }

            var cacheExpiryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddDays(1),
            };

            _cache.Set(symbol.Name, result, cacheExpiryOptions);

            return result;
        }
    }
}
