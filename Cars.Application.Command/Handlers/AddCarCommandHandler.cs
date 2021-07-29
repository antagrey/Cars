using System.Threading.Tasks;
using Cars.Application.Command.Results;
using Cars.Domain;
using Cars.Domain.Contracts;
using Cars.Infrastructure.Command;
using Cars.Infrastructure.EntityFramework.Repository;

namespace Cars.Application.Command.Handlers
{
    public class AddCarCommandHandler : IAsyncCommandHandler<AddCarCommand, CarAddedResult>
    {
        private readonly IRepository<Car> carRepository;

        public AddCarCommandHandler(IRepository<Car> carRepository)
        {
            this.carRepository = carRepository;
        }

        public CarAddedResult Result { get; private set; }

        public async Task HandleAsync(AddCarCommand command)
        {
            var car = new Car(command);

            carRepository.Add(car);

            await carRepository.SaveChangesAsync();

            Result = new CarAddedResult(car.Id);
        }
    }
}
