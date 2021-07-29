using System.Collections.Generic;
using System.Data;
using AutoFixture;
using Moq;
using Cars.Application.Query.Handlers;
using Cars.Infrastructure.DataAccess;
using Cars.Application.Query.Queries;
using Cars.Application.Providers;
using Xunit;
using System.Threading.Tasks;
using Cars.Application.Query.Dtos;

namespace Cars.Application.Query.UnitTest.Handlers
{
    public class GetCarByIdQueryHandlerTests
    {
        private readonly Fixture fixture;

        private readonly Mock<IDapperConnection> dapperConnection;

        private readonly GetCarByIdQueryHandler sut;

        public GetCarByIdQueryHandlerTests()
        {
            fixture = new Fixture();
            var connectionStringsProvider = new Mock<IConnectionStringsProvider>();
            var dapperConnectionFactory = new Mock<IDapperConnectionFactory>();
            dapperConnection = new Mock<IDapperConnection>();

            sut = new GetCarByIdQueryHandler(
                connectionStringsProvider.Object,
                dapperConnectionFactory.Object);

            var carConnectionString = fixture.Create<string>();

            connectionStringsProvider
                .Setup(p => p.Cars)
                .Returns(carConnectionString);

            dapperConnectionFactory
                .Setup(f => f.CreateConnection(carConnectionString))
                .Returns(dapperConnection.Object);
        }

        [Fact]
        public async Task HandleAsync_WhenCarIsNotFound_ShouldReturnNull()
        {
            // Arrange
            var query = fixture.Create<GetCarByIdQuery>();

            // Act
            var result = await sut.HandleAsync(query);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task HandleAsync_WhenCarIsFound_ShouldReturnCarResult()
        {
            // Arrange
            var query = fixture.Create<GetCarByIdQuery>();
            var carDto = fixture.Create<CarDto>();

            dapperConnection
                .Setup(c => c.QueryAsync<CarDto>("dbo.usp_Read_Car_By_Id", CommandType.StoredProcedure, query))
                .ReturnsAsync(new List<CarDto> { carDto });

            // Act
            var result = await sut.HandleAsync(query);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(carDto.Id, result.Id);
            Assert.Equal(carDto.Make, result.Make);
            Assert.Equal(carDto.Model, result.Model);
            Assert.Equal(carDto.Colour, result.Colour);
            Assert.Equal(carDto.Year, result.Year);
        }
    }
}
