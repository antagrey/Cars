using Cars.Infrastructure.Command;

namespace Cars.Domain.Contracts
{
    public class AddCarCommand : ICommand
    {
        public AddCarCommand(
            string make,
            string model,
            string colour,
            int year)
        {
            Make = make;
            Model = model;
            Colour = colour;
            Year = year;
        }

        public string Make { get; }
        public string Model { get; }
        public string Colour { get; }
        public int Year { get; }
    }
}
