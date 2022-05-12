using Microsoft.Extensions.DependencyInjection;

namespace Knab.CodingAssessment.Seedwork.Queries
{
    public class QueryBus : IQueryBus
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryBus(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TQueryResult> Dispatch<TQueryFilter, TQueryResult>(TQueryFilter filter)
            where TQueryFilter : IQueryFilter
            where TQueryResult : IQueryResult
        {
            var handler = _serviceProvider.GetRequiredService<IQueryHandler<TQueryFilter, TQueryResult>>();
            var result = await handler.HandleAsync(filter);
            return result;
        }
    }
}


