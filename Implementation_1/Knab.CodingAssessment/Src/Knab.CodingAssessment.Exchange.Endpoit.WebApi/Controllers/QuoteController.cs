using Knab.CodingAssessment.Exchange.ApplicationServices.Contracts.Queries;
using Knab.CodingAssessment.Exchange.Endpoint.WebApi.Filters;
using Knab.CodingAssessment.Seedwork.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Knab.CodingAssessment.Exchange.Endpoint.WebApi.Controllers
{
    [ApiController]
    [Route("quotes")]
    public class QuoteController : ControllerBase
    {

        private readonly ILogger<QuoteController> _logger;
        private readonly IQueryBus _queryBus;
        private readonly IConfiguration _configuration;

        public QuoteController(ILogger<QuoteController> logger, IQueryBus queryBus, IConfiguration configuration)
        {
            _logger = logger;
            _queryBus = queryBus;
            _configuration = configuration;
        }

        //TODO: Validate input
        [HttpGet("{cryptoAbbreviation}/latest")]
        public async Task<ApiResult<CollectionQueryResult<LatestQuoteOfCryptoQueryResult>>> Get([FromRoute] string cryptoAbbreviation)
        {
            //
            var quoteCurrencies = _configuration.GetSection("QuoteCurrencies").Get<List<string>>();

            var tasks = quoteCurrencies.AsParallel()
                .Select(q => GetLatestQuoteOf(cryptoAbbreviation, q));

            var result = await Task.WhenAll(tasks);

            var collection = new CollectionQueryResult<LatestQuoteOfCryptoQueryResult>(result.Where(i => i != null).ToList());
            return ApiResult<CollectionQueryResult<LatestQuoteOfCryptoQueryResult>>.Ok(collection);
        }

        private Task<LatestQuoteOfCryptoQueryResult> GetLatestQuoteOf(string baseCurrency, string quoteCurrency)
        {
            var filter = new GetLatestQuoteOfCryptoQueryFilter() { BaseCurrency = baseCurrency, QuoteCurrency = quoteCurrency };
            return _queryBus.Dispatch<GetLatestQuoteOfCryptoQueryFilter, LatestQuoteOfCryptoQueryResult>(filter);
        }
    }
}