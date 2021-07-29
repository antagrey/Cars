using Cars.Application.Query.Results;
using Cars.Infrastructure.Query;

namespace Cars.Application.Query.Queries
{
    public class GetCarByIdQuery : IQuery<CarResult>
    {
        public GetCarByIdQuery(int carId)
        {
            CarId = carId;
        }

        public int CarId { get; }
    }
}
