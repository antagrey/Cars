using System.Threading.Tasks;
using Cars.Application.Command.Results;
using Cars.Domain;
using Cars.Domain.Contracts;
using Cars.Infrastructure.Command;
using Cars.Infrastructure.EntityFramework.Repository;

namespace Cars.Application.Command.Handlers
{
    public class ChangeCarCommandHandler : IAsyncCommandHandler<ChangeCarCommand, CarChangedResult>
    {
        private readonly IRepository<Car> carRepository;

        public ChangeCarCommandHandler(IRepository<Car> carRepository)
        {
            this.carRepository = carRepository;
        }

        public CarChangedResult Result { get; private set; }

        public async Task HandleAsync(ChangeCarCommand command)
        {
            var car = await carRepository.FindByIdAsync(command.CarId);

            if (car != null)
            {
                car.Change(command);

                await carRepository.SaveChangesAsync();
            }

            Result = new CarChangedResult(car != null);
        }
    }
}
