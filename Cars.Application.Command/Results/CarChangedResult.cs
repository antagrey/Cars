using Cars.Infrastructure.Command;

namespace Cars.Application.Command.Results
{
    public class CarChangedResult : CommandResult
    {
        public CarChangedResult(bool carFound)
        {
            CarFound = carFound;
        }

        public bool CarFound { get; }
    }
}
