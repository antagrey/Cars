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
    public class AddCarCommandHandlerTests
    {
        private readonly Fixture fixture;
        private readonly Mock<IRepository<Car>> carRepository;
        private readonly AddCarCommandHandler sut;

        public AddCarCommandHandlerTests()
        {
            fixture = new Fixture();

            carRepository = new Mock<IRepository<Car>>();

            sut = new AddCarCommandHandler(carRepository.Object);
        }

        [Fact]
        public async Task HandleAsync_ShouldAddNewEntityToRepository_SaveChanges_AndSetResult()
        {
            // Arrange
            var command = fixture.Create<AddCarCommand>();

            // Act
            await sut.HandleAsync(command);

            // Assert
            carRepository
                .Verify(x => x.Add(It.Is<Car>(c =>
                    c.Make == command.Make
                )), Times.Once);

            carRepository.Verify(x => x.SaveChangesAsync(), Times.Once);

            Assert.NotNull(sut.Result);
        }
    }
}
