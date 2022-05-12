using Knab.CodingAssessment.Seedwork.Domain;
using Knab.CodingAssessment.Seedwork.Queries;

namespace Knab.CodingAssessment.Exchange.ApplicationServices.Contracts.Queries
{
    public class LatestQuoteOfCryptoQueryResult : IQueryResult
    {
        public string Symbol { get; set; }
        public DateTime LastUpdate { get; set; }
        public decimal Price { get; set; }
    }

}
