using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Cars.Application.Providers;
using Cars.Application.Query.Dtos;
using Cars.Application.Query.Queries;
using Cars.Application.Query.Results;
using Cars.Infrastructure.DataAccess;
using Cars.Infrastructure.Query;

namespace Cars.Application.Query.Handlers
{
    public class GetCarByIdQueryHandler : IAsyncQueryHandler<GetCarByIdQuery, CarResult>
    {
        private readonly IConnectionStringsProvider connectionStringsProvider;
        private readonly IDapperConnectionFactory dapperConnectionFactory;

        public GetCarByIdQueryHandler(
            IConnectionStringsProvider connectionStringsProvider,
            IDapperConnectionFactory dapperConnectionFactory)
        {
            this.connectionStringsProvider = connectionStringsProvider;
            this.dapperConnectionFactory = dapperConnectionFactory;
        }

        public async Task<CarResult> HandleAsync(GetCarByIdQuery query)
        {
            const string sql = "dbo.usp_Read_Car_By_Id";

            using var dapperConnection = dapperConnectionFactory.CreateConnection(connectionStringsProvider.Cars);

            var carDto = (await dapperConnection.QueryAsync<CarDto>(sql, CommandType.StoredProcedure, query)).SingleOrDefault();

            if (carDto == null)
            {
                return null;
            }

            return new CarResult(
                carDto.Id,
                carDto.Make,
                carDto.Model,
                carDto.Colour,
                carDto.Year);
        }
    }
}
