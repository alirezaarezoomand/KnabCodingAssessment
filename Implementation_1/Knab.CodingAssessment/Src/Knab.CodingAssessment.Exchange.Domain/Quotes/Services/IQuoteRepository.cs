using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knab.CodingAssessment.Exchange.Domain.Quotes.Services
{
    public interface IQuoteRepository
    {
        Task<Quote> GetLatestQuoteOf(Symbol symbol);
    }
}
