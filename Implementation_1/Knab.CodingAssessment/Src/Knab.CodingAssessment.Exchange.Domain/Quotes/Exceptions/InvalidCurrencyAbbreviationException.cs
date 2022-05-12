using Knab.CodingAssessment.Exchange.Common;
using Knab.CodingAssessment.Seedwork.Domain;

namespace Knab.CodingAssessment.Exchange.Domain.Quotes.Exceptions
{
    public class InvalidCurrencyAbbreviationException : DomainException
    {
        public InvalidCurrencyAbbreviationException() : base(ExceptionMessages.InvalidCurrencyAbbreviationException)
        {
        }
    }
}
