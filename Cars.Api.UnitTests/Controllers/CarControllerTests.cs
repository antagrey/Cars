using System.Threading.Tasks;
using AutoFixture;
using Cars.Api.Controllers;
using Cars.Api.Requests;
using Cars.Api.Responses;
using Cars.Application.Command.Results;
using Cars.Application.Query.Queries;
using Cars.Application.Query.Results;
using Cars.Domain.Contracts;
using Cars.Infrastructure.Command;
using Cars.Infrastructure.Query;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Cars.Api.UnitTests.Controllers
{
    public class CarControllerTests
    {
        private readonly Fixture fixture;
        private readonly Mock<ILogger<CarController>> logger;
        private readonly Mock<IAsyncQueryHandler<GetCarByIdQuery, CarResult>> getCarByIdQueryHandler;
        private readonly Mock<IAsyncCommandHandler<AddCarCommand, CarAddedResult>> addCarCommandHandler;
        private readonly Mock<IAsyncCommandHandler<RemoveCarCommand, CarRemovedResult>> removeCarCommandHandler;
        private readonly Mock<IAsyncCommandHandler<ChangeCarCommand, CarChangedResult>> changeCarCommandHandler;

        private readonly CarController sut;

        public CarControllerTests()
        {
            fixture = new Fixture();
            logger = new Mock<ILogger<CarController>>();
            getCarByIdQueryHandler = new Mock<IAsyncQueryHandler<GetCarByIdQuery, CarResult>>();
            addCarCommandHandler = new Mock<IAsyncCommandHandler<AddCarCommand, CarAddedResult>>();
            removeCarCommandHandler = new Mock<IAsyncCommandHandler<RemoveCarCommand, CarRemovedResult>>();
            changeCarCommandHandler = new Mock<IAsyncCommandHandler<ChangeCarCommand, CarChangedResult>>();

            sut = new CarController(
                logger.Object,
                getCarByIdQueryHandler.Object,
                addCarCommandHandler.Object,
                removeCarCommandHandler.Object,
                changeCarCommandHandler.Object);
        }

        [Fact]
        public async Task Get_ShouldGetCarDetails_AndReturnOkWithCarResponse()
        {
            // Arrange
            var carId = fixture.Create<int>();
            var carResult = fixture.Create<CarResult>();
            getCarByIdQueryHandler.Setup(c => c.HandleAsync(It.Is<GetCarByIdQuery>(q => q.CarId == carId)))
                .ReturnsAsync(carResult);

            // Act
            var result = await sut.Get(carId) as OkObjectResult;
            var response = result?.Value as GetCarResponse;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(response);
            Assert.IsType<GetCarResponse>(result.Value);
            Assert.Equal(carResult.Make, response.Make);
            Assert.Equal(carResult.Model, response.Model);
            Assert.Equal(carResult.Colour, response.Colour);
            Assert.Equal(carResult.Year, response.Year);
        }

        [Fact]
        public async Task Get_WhenInvalidCarId_ShouldReturnBadGateway()
        {
            // Arrange
            var carId = 0;

            // Act
            var result = await sut.Get(carId) as BadRequestResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Get_WhenCarCannotBeFound_ShouldReturnNotFound()
        {
            // Arrange
            var carId = fixture.Create<int>();

            // Act
            var result = await sut.Get(carId) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Post_ShouldAddCar_AndReturnId()
        {
            // Arrange
            var request = fixture.Create<AddCarRequest>();
            var carResult = fixture.Create<CarAddedResult>();
            addCarCommandHandler.Setup(c => c.HandleAsync(It.Is<AddCarCommand>(cmd =>
                    cmd.Make == request.Make &&
                    cmd.Model == request.Model &&
                    cmd.Colour == request.Colour &&
                    cmd.Year == request.Year)))
                .Callback(() => addCarCommandHandler.SetupGet(x => x.Result).Returns(carResult));

            // Act
            var result = await sut.Post(request) as OkObjectResult;
            var response = result?.Value as AddCarResponse;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(response);
            Assert.Equal(carResult.Id, response.Id);
        }

        [Fact]
        public async Task Post_WhenRequestIsNull_ShouldReturnBadGateway()
        {
            // Arrange
            AddCarRequest request = null;

            // Act
            var result = await sut.Post(request) as BadRequestResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Put_ShouldUpdateCar_AndReturn()
        {
            // Arrange
            var request = fixture.Create<ChangeCarRequest>();
            var carResult = new CarChangedResult(true);
            changeCarCommandHandler.Setup(c => c.HandleAsync(It.Is<ChangeCarCommand>(cmd =>
                    cmd.Make == request.Make &&
                    cmd.Model == request.Model &&
                    cmd.Colour == request.Colour &&
                    cmd.Year == request.Year)))
                .Callback(() => changeCarCommandHandler.SetupGet(x => x.Result).Returns(carResult));

            // Act
            var result = await sut.Put(request) as OkResult;

            // Assert
            Assert.NotNull(result);
            changeCarCommandHandler.Verify(c => c.HandleAsync(It.Is<ChangeCarCommand>(cmd =>
                cmd.Make == request.Make &&
                cmd.Model == request.Model &&
                cmd.Colour == request.Colour &&
                cmd.Year == request.Year)), Times.Once);
        }

        [Fact]
        public async Task Put_WhenRequestIsNull_ShouldReturnBadGateway()
        {
            // Arrange
            ChangeCarRequest request = null;

            // Act
            var result = await sut.Put(request) as BadRequestResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Put_WhenCarNotFound_ShouldReturnNotFound()
        {
            // Arrange
            var request = fixture.Create<ChangeCarRequest>();
            var carResult = new CarChangedResult(false);
            changeCarCommandHandler.Setup(c => c.HandleAsync(It.Is<ChangeCarCommand>(cmd =>
                    cmd.Make == request.Make &&
                    cmd.Model == request.Model &&
                    cmd.Colour == request.Colour &&
                    cmd.Year == request.Year)))
                .Callback(() => changeCarCommandHandler.SetupGet(x => x.Result).Returns(carResult));

            // Act
            var result = await sut.Put(request) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_ShouldRemoveCar_AndReturn()
        {
            // Arrange
            var carId = fixture.Create<int>();
            var carResult = new CarRemovedResult(true);
            removeCarCommandHandler.Setup(c => c.HandleAsync(It.Is<RemoveCarCommand>(cmd =>
                    cmd.CarId == carId)))
                .Callback(() => removeCarCommandHandler.SetupGet(x => x.Result).Returns(carResult));

            // Act
            var result = await sut.Delete(carId) as OkResult;

            // Assert
            Assert.NotNull(result);
            removeCarCommandHandler.Verify(c => c.HandleAsync(It.Is<RemoveCarCommand>(cmd =>
                cmd.CarId == carId)), Times.Once);
        }

        [Fact]
        public async Task Delete_WhenInvalidCarId_ShouldReturnBadGateway()
        {
            // Arrange
            var carId = 0;

            // Act
            var result = await sut.Delete(carId) as BadRequestResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_WhenCarNotFound_ShouldReturnNotFound()
        {
            // Arrange
            var carId = fixture.Create<int>();
            var carResult = new CarRemovedResult(false);
            removeCarCommandHandler.Setup(c => c.HandleAsync(It.Is<RemoveCarCommand>(cmd =>
                    cmd.CarId == carId)))
                .Callback(() => removeCarCommandHandler.SetupGet(x => x.Result).Returns(carResult));

            // Act
            var result = await sut.Delete(carId) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }
    }
}
