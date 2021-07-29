using Cars.Infrastructure.Contracts;
using System.Threading.Tasks;

namespace Cars.Infrastructure.Query
{
    public interface IAsyncQueryHandler<in TQuery, TResult> where TQuery : IQuery<TResult> where TResult : QueryResult
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}
