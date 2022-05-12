﻿namespace Knab.CodingAssessment.Exchage.DataProvider
{
    public record QuotesResponse
    {
        public string BaseCurrency { get; set; }
        public string QuoteCurrency { get; set; }
        public decimal Price { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}