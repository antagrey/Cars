using Cars.Infrastructure.Command;

namespace Cars.Application.Command.Results
{
    public class CarAddedResult : CommandResult
    {
        public CarAddedResult(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
