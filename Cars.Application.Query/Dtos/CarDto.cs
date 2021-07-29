using Cars.Infrastructure.Contracts;

namespace Cars.Application.Query.Dtos
{
    public class CarDto : Dto
    {
        public int Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Colour { get; set; }
        public int Year { get; set; }
    }
}
