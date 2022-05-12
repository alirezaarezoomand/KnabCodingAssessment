using Knab.CodingAssessment.Seedwork.Queries;

namespace Knab.CodingAssessment.Exchange.ApplicationServices.Contracts.Queries
{
    public class GetLatestQuoteOfCryptoQueryFilter : IQueryFilter
    {
        public string BaseCurrency { get; set; }
        public string QuoteCurrency { get; set; }
    }
}
