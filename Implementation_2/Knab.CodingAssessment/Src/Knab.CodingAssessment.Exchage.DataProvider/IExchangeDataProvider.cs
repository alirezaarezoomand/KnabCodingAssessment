namespace Knab.CodingAssessment.Exchage.DataProvider
{
    public interface IExchangeDataProvider
    {
        Task<QuotesResponse> GetLatestQuotesAsync(string baseCurrency, string quoteCurrency);
    }
}