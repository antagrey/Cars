using System.Threading.Tasks;
using Cars.Domain;
using Cars.Domain.Contracts;
using Cars.Infrastructure.Command;
using Cars.Infrastructure.EntityFramework.Repository;

namespace Cars.Application.Command.Handlers
{
    public class RemoveCarCommandHandler : IAsyncOneWayCommandHandler<RemoveCarCommand>
    {
        private readonly IRepository<Car> carRepository;

        public RemoveCarCommandHandler(IRepository<Car> carRepository)
        {
            this.carRepository = carRepository;
        }

        public async Task HandleAsync(RemoveCarCommand command)
        {
            var car = await carRepository.FindByIdAsync(command.CarId);

            if (car == null)
            {
                return;
            }

            carRepository.Remove(car);

            await carRepository.SaveChangesAsync();
        }
    }
}
