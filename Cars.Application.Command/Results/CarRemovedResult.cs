using Cars.Infrastructure.Command;

namespace Cars.Application.Command.Results
{
    public class CarRemovedResult : CommandResult
    {
        public CarRemovedResult(bool carFound)
        {
            CarFound = carFound;
        }

        public bool CarFound { get; }
    }
}
