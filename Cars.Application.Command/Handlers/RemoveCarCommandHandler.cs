using System.Threading.Tasks;
using Cars.Application.Command.Results;
using Cars.Domain;
using Cars.Domain.Contracts;
using Cars.Infrastructure.Command;
using Cars.Infrastructure.EntityFramework.Repository;

namespace Cars.Application.Command.Handlers
{
    public class RemoveCarCommandHandler : IAsyncCommandHandler<RemoveCarCommand, CarRemovedResult>
    {
        private readonly IRepository<Car> carRepository;

        public RemoveCarCommandHandler(IRepository<Car> carRepository)
        {
            this.carRepository = carRepository;
        }

        public CarRemovedResult Result { get; private set; }

        public async Task HandleAsync(RemoveCarCommand command)
        {
            var car = await carRepository.FindByIdAsync(command.CarId);

            if (car != null)
            {
                carRepository.Remove(car);

                await carRepository.SaveChangesAsync(); ;
            }

            Result = new CarRemovedResult(car != null);
        }
    }
}
