namespace Knab.CodingAssessment.Seedwork.Queries
{
    public interface IQueryBus
    {
        Task<TQueryResult> Dispatch<TQueryFilter, TQueryResult>(TQueryFilter filter)
            where TQueryFilter : IQueryFilter
            where TQueryResult : IQueryResult;
    }
}


