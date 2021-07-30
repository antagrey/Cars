using Cars.Infrastructure.Command;

namespace Cars.Domain.Contracts
{
    public class RemoveCarCommand : ICommand
    {
        public RemoveCarCommand(
            int carId)
        {
            CarId = carId;
        }

        public int CarId { get; }
    }
}
