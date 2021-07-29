using Cars.Infrastructure.Command;

namespace Cars.Domain.Contracts
{
    public class ChangeCarCommand : ICommand
    {
        public ChangeCarCommand(
            int carId,
            string make,
            string model,
            string colour,
            int year)
        {
            CarId = carId;
            Make = make;
            Model = model;
            Colour = colour;
            Year = year;
        }

        public int CarId { get; }
        public string Make { get; }
        public string Model { get; }
        public string Colour { get; }
        public int Year { get; }
    }
}
