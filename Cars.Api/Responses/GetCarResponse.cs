using Cars.Application.Query.Results;

namespace Cars.Api.Responses
{
    public class GetCarResponse
    {
        public GetCarResponse(CarResult car)
        {
            Make = car.Make;
            Model = car.Model;
            Colour = car.Colour;
            Year = car.Year;
        }

        public string Make { get; private set; }
        public string Model { get; private set; }
        public string Colour { get; private set; }
        public int Year { get; private set; }
    }
}
