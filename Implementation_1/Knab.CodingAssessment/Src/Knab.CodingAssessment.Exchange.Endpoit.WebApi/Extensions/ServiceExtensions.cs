using Knab.CodingAssessment.Exchange.ApplicationServices.Contracts.Queries;
using Knab.CodingAssessment.Exchange.ApplicationServices.QueryHandlers;
using Knab.CodingAssessment.Exchange.Domain.Quotes.Services;
using Knab.CodingAssessment.Exchange.Repositories.Quotes;
using Knab.CodingAssessment.Seedwork.Queries;

namespace Knab.CodingAssessment.Exchange.Endpoint.WebApi.Extensions
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddQueryHandlerServices(this IServiceCollection services)
        {
            services.AddTransient<IQueryHandler<GetLatestQuoteOfCryptoQueryFilter, LatestQuoteOfCryptoQueryResult>, GetLatestQuoteOfCryptoQueryHandler>();

            return services;
        }

        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddTransient<IQuoteRepository, QuoteRepository>();
            services.Decorate<IQuoteRepository, QuoteRepositoryWithFailover>();

            return services;
        }

    }
}
