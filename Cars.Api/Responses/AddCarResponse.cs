using Cars.Application.Command.Results;

namespace Cars.Api.Responses
{
    public class AddCarResponse
    {
        public AddCarResponse(CarAddedResult car)
        {
            Id = car.Id;
        }

        public int Id { get; private set; }
    }
}
