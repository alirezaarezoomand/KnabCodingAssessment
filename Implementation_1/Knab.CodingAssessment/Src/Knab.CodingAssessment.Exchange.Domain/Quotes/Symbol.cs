namespace Knab.CodingAssessment.Exchange.Domain.Quotes
{
    public class Symbol
    {
        public Symbol(Currency baseCurrency, Currency quoteCurrency)
        {
            BaseCurrency = baseCurrency;
            QuoteCurrency = quoteCurrency;
        }

        public Currency BaseCurrency { get; private set; }
        public Currency QuoteCurrency { get; private set; }
        public string Name { get { 
                return String.Format("{0}{1}", BaseCurrency.Abbreviation, QuoteCurrency.Abbreviation); 
            }  
        }
    }
}
