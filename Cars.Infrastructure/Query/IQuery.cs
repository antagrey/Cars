using Cars.Infrastructure.Contracts;

namespace Cars.Infrastructure.Query
{
    public interface IQuery<TResult> where TResult : QueryResult
    {
    }
}
