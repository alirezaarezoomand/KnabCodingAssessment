using Knab.CodingAssessment.Exchage.DataProvider;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace Knab.CodingAssessment.DataProvider.CoinMarketCap
{

    public class CoinMarketCapServiceClient : IExchangeDataProvider
    {
        private readonly HttpClient _httpClient;
        private readonly CoinMarketCapOptions _options;
        private readonly ILogger<CoinMarketCapServiceClient> _logger;

        public CoinMarketCapServiceClient(HttpClient httpClient, IOptions<CoinMarketCapOptions> options, ILogger<CoinMarketCapServiceClient> logger)
        {
            _httpClient = httpClient;
            _options = options.Value;
            _logger = logger;
        }

        public async Task<QuotesResponse> GetLatestQuotesAsync(string baseCurrency, string quoteCurrency)
        {
            var requestId = Guid.NewGuid().ToString();
            _logger.LogInformation("Begin", requestId);
            try
            {
                var response = await CallApi(baseCurrency, quoteCurrency);
                response.EnsureSuccessStatusCode();

                var result = await ToQuoteResponse(baseCurrency, quoteCurrency, response);

                _logger.LogInformation("End", requestId);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString(), requestId);
                return null;
            }            
        }

        private async Task<HttpResponseMessage> CallApi(string baseCurrency, string quoteCurrency)
        {
            var uriBuilder = new StringBuilder();
            uriBuilder.Append(_options.ApiUrl);
            uriBuilder.Append($"cryptocurrency/quotes/latest?symbol={baseCurrency}&convert={quoteCurrency}");
            var latestQuotesUri = uriBuilder.ToString();

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, latestQuotesUri);
            httpRequestMessage.Headers.Add("X-CMC_PRO_API_KEY", _options.ApiKey);

            var response = await _httpClient.SendAsync(httpRequestMessage);
            return response;
        }

        private async Task<QuotesResponse> ToQuoteResponse(string baseCurrency, string quoteCurrency, HttpResponseMessage response)
        {
            var responseContent = await response.Content.ReadAsStringAsync();

            var jsonElement = JsonSerializer.Deserialize<JsonElement>(responseContent);

            var price = jsonElement.GetProperty("data").GetProperty(baseCurrency)[0].GetProperty("quote").GetProperty(quoteCurrency).GetProperty("price").GetDecimal();
            var lastUpdate = jsonElement.GetProperty("data").GetProperty(baseCurrency)[0].GetProperty("quote").GetProperty(quoteCurrency).GetProperty("last_updated").GetDateTime();

            var result = new QuotesResponse()
            {
                BaseCurrency = baseCurrency,
                QuoteCurrency = quoteCurrency,
                LastUpdate = lastUpdate,
                Price = price
            };

            return result;
        }
    }
}