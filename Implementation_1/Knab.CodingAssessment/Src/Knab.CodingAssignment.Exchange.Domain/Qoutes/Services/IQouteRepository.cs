using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knab.CodingAssessment.Exchange.Domain.Qoutes.Services
{
    public interface IQouteRepository
    {
        Task<IEnumerable<Qoute>> GetLatestQoutesOf(Currency currency);
    }
}
