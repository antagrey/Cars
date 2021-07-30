using System.Threading.Tasks;
using AutoFixture;
using Cars.Application.Command.Handlers;
using Cars.Domain;
using Cars.Domain.Contracts;
using Cars.Infrastructure.EntityFramework.Repository;
using Moq;
using Xunit;

namespace Cars.Application.Command.UnitTest.Handlers
{
    public class RemoveCarCommandHandlerTests
    {
        private readonly Fixture fixture;
        private readonly Mock<IRepository<Car>> carRepository;
        private readonly RemoveCarCommandHandler sut;

        public RemoveCarCommandHandlerTests()
        {
            fixture = new Fixture();

            carRepository = new Mock<IRepository<Car>>();

            sut = new RemoveCarCommandHandler(carRepository.Object);
        }

        [Fact]
        public async Task HandleAsync_ShouldRemoveCar_AndSaveChanges()
        {
            // Arrange
            var command = fixture.Create<RemoveCarCommand>();
            var car = fixture.Create<Car>();

            carRepository
                .Setup(x => x.FindByIdAsync(command.CarId))
                .ReturnsAsync(car);

            // Act
            await sut.HandleAsync(command);

            // Assert
            carRepository
                .Verify(x => x.Remove(car), Times.Once);

            carRepository
                .Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task HandleAsync_WhenNoCarCanBeFound_ShouldExitWithoutRemovingOrSaving()
        {
            // Arrange
            var command = fixture.Create<RemoveCarCommand>();

            carRepository
                .Setup(x => x.FindByIdAsync(command.CarId))
                .ReturnsAsync(default(Car));

            // Act
            await sut.HandleAsync(command);

            // Assert
            carRepository
                .Verify(x => x.Remove(It.IsAny<Car>()), Times.Never);

            carRepository
                .Verify(x => x.SaveChangesAsync(), Times.Never);
        }
    }
}
