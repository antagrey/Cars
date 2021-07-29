using Cars.Infrastructure.Query;

namespace Cars.Application.Query.Results
{
    public class CarResult : QueryResult
    {
        public CarResult(int id, string make, string model, string colour, int year)
        {
            Id = id;
            Make = make;
            Model = model;
            Colour = colour;
            Year = year;
        }

        public int Id { get; }
        public string Make { get; }
        public string Model { get; }
        public string Colour { get; }
        public int Year { get; }
    }
}
