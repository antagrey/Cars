using System.Threading.Tasks;
using AutoFixture;
using Cars.Application.Command.Handlers;
using Cars.Domain;
using Cars.Domain.Contracts;
using Cars.Infrastructure.EntityFramework.Repository;
using Moq;
using Xunit;
using static Cars.Application.Command.Constants.CommandConstants;

namespace Cars.Application.Command.UnitTest.Handlers
{
    public class ChangeCarCommandHandlerTests
    {
        private readonly Fixture fixture;
        private readonly Mock<IRepository<Car>> carRepository;
        private readonly ChangeCarCommandHandler sut;

        public ChangeCarCommandHandlerTests()
        {
            fixture = new Fixture();

            carRepository = new Mock<IRepository<Car>>();

            sut = new ChangeCarCommandHandler(carRepository.Object);
        }

        [Fact]
        public async Task HandleAsync_ShouldFindCar_CallChange_AndSaveChanges()
        {
            // Arrange
            var command = fixture.Create<ChangeCarCommand>();
            var car = fixture.Create<Car>();

            carRepository
                .Setup(x => x.FindByIdAsync(command.CarId))
                .ReturnsAsync(car);

            // Act
            await sut.HandleAsync(command);

            // Assert
            Assert.Equal(command.Make, car.Make);
            Assert.Equal(command.Model, car.Model);
            Assert.Equal(command.Colour, car.Colour);
            Assert.Equal(command.Year, car.Year);

            carRepository
                .Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task HandleAsync_WhenCarCannotBeFound_ShouldThrow()
        {
            // Arrange
            var command = fixture.Create<ChangeCarCommand>();

            Car car = null;
            carRepository
                .Setup(x => x.FindByIdAsync(command.CarId))
                .ReturnsAsync(car);

            var expectedException = string.Format(AggregateCouldNotBeFound, nameof(Car), command.CarId);

            // Act
            var ex = await Record.ExceptionAsync(() => sut.HandleAsync(command));

            // Assert
            Assert.NotNull(ex);
            Assert.Equal(expectedException, ex.Message);

            carRepository.Verify(x => x.SaveChangesAsync(), Times.Never);
        }
    }
}
