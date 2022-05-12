using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knab.CodingAssessment.Exchange.Domain.Quotes
{
    public class Quote
    {
        public Quote(Symbol symbol, DateTime date, decimal price)
        {
            Symbol = symbol;
            Date = date;
            Price = price;
        }

        public Symbol Symbol { get; private set; }
        public DateTime Date { get; private set; }
        public decimal Price { get; private set; }
    }
}
