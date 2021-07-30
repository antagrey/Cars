using System;
using System.Threading.Tasks;
using Cars.Domain;
using Cars.Domain.Contracts;
using Cars.Infrastructure.Command;
using Cars.Infrastructure.EntityFramework.Repository;
using static Cars.Application.Command.Constants.CommandConstants;

namespace Cars.Application.Command.Handlers
{
    public class ChangeCarCommandHandler : IAsyncOneWayCommandHandler<ChangeCarCommand>
    {
        private readonly IRepository<Car> carRepository;

        public ChangeCarCommandHandler(IRepository<Car> carRepository)
        {
            this.carRepository = carRepository;
        }

        public async Task HandleAsync(ChangeCarCommand command)
        {
            var car = await carRepository.FindByIdAsync(command.CarId);

            if (car == null)
            {
                throw new Exception(string.Format(AggregateCouldNotBeFound, nameof(Car), command.CarId));
            }

            car.Change(command);

            await carRepository.SaveChangesAsync();
        }
    }
}
