using Knab.CodingAssessment.Exchange.Common;
using Knab.CodingAssessment.Seedwork.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knab.CodingAssessment.Exchange.Domain.Qoutes.Exceptions
{
    public class InvalidCurrencyAbbreviationException : DomainException
    {
        public InvalidCurrencyAbbreviationException() : base(ExceptionMessages.InvalidCurrencyAbbreviationException)
        {
        }
    }
}
