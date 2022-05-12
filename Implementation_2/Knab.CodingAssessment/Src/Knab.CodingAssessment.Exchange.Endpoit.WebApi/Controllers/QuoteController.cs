using Knab.CodingAssessment.Exchage.DataProvider;
using Microsoft.AspNetCore.Mvc;

namespace Knab.CodingAssessment.Exchange.Endpoint.WebApi.Controllers
{
    [ApiController]
    [Route("quotes")]
    public class QuoteController : ControllerBase
    {

        private readonly ILogger<QuoteController> _logger;
        private readonly IExchangeDataProvider _exchangeDataProvider;
        private readonly IConfiguration _configuration;

        public QuoteController(ILogger<QuoteController> logger, IConfiguration configuration, IExchangeDataProvider exchangeDataProvider)
        {
            _logger = logger;
            _configuration = configuration;
            _exchangeDataProvider = exchangeDataProvider;
        }

        //TODO: Validate input
        [HttpGet("{cryptoAbbreviation}/latest")]
        public async Task<ApiResult<IEnumerable<QuotesResponse>>> Get([FromRoute] string cryptoAbbreviation)
        {
            //
            var quoteCurrencies = _configuration.GetSection("QuoteCurrencies").Get<List<string>>();

            var tasks = quoteCurrencies.AsParallel()
                .Select(q => GetLatestQuoteOf(cryptoAbbreviation, q));

            var result = await Task.WhenAll(tasks);

            var collection = result.Where(i => i != null).ToList();
            return ApiResult<IEnumerable<QuotesResponse>>.Ok(collection);
        }

        private Task<QuotesResponse> GetLatestQuoteOf(string baseCurrency, string quoteCurrency)
        {
            return _exchangeDataProvider.GetLatestQuotesAsync(baseCurrency, quoteCurrency);
        }
    }
}