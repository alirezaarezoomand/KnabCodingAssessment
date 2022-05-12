using Knab.CodingAssessment.Exchange.Domain.Quotes.Exceptions;

namespace Knab.CodingAssessment.Exchange.Domain.Quotes
{
    public class Currency
    {
        private Currency(CurrencyType type, string abbreviation)
        {
            Type = type;
            SetAbbreviation(abbreviation);
        }
        public CurrencyType Type { get; private set; }
        public string Abbreviation { get; private set; }


        //TODO: Validate format of currency abbreviation, exsitence of abbreviation and ...
        private void SetAbbreviation(string abbreviation)
        {
            if(string.IsNullOrEmpty(abbreviation) || abbreviation.Length < 3)
            {
                throw new InvalidCurrencyAbbreviationException();
            }
            Abbreviation = abbreviation.Trim().ToUpper();
        }

        public static Currency CreateFiat(string abbreviation)
        {
            return new Currency(CurrencyType.Fiat, abbreviation);
        }

        public static Currency CreateCrypto(string abbreviation)
        {
            return new Currency(CurrencyType.Crypto, abbreviation);
        }
    }
}
