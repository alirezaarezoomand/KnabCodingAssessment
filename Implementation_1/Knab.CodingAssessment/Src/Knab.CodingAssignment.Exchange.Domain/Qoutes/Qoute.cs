using Knab.CodingAssessment.Exchange.Domain.Qoutes.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knab.CodingAssessment.Exchange.Domain.Qoutes
{
    public class Qoute
    {
        public Qoute(Symbol symbol, DateTime date, decimal rate)
        {
            Symbol = symbol;
            Date = date;
            Rate = rate;
        }

        public Symbol Symbol { get; private set; }
        public DateTime Date { get; private set; }
        public decimal Rate { get; set; }
    }

    public class Symbol
    {
        public Symbol(Currency baseCurrency, Currency qouteCurrency)
        {
            BaseCurrency = baseCurrency;
            QouteCurrency = qouteCurrency;
        }

        public Currency BaseCurrency { get; private set; }
        public Currency QouteCurrency { get; private set; }
        public string Name { get { 
                return String.Format("{0}{1}", BaseCurrency.Abbreviation, QouteCurrency.Abbreviation); 
            }  
        }
    }

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

    public enum CurrencyType
    {
        Crypto = 1,
        Fiat = 2
    }
}
