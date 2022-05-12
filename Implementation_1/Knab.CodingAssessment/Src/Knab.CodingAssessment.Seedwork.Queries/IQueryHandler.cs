using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Knab.CodingAssessment.Seedwork.Queries
{
    public interface IQueryHandler<TQueryFilter, TQueryResult>
            where TQueryFilter : IQueryFilter
            where TQueryResult : IQueryResult
    {
        Task<TQueryResult> HandleAsync(TQueryFilter filter);

    }
}


