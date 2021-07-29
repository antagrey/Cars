using AutoFixture;
using Cars.Domain.Contracts;
using Xunit;

namespace Cars.Domain.UnitTests
{
    public class CarTests
    {
        private readonly Fixture fixture = new Fixture();

        [Fact]
        public void Construct_ShouldSetProperties()
        {
            // Arrange
            var command = fixture.Create<AddCarCommand>();

            // Act
            var sut = new Car(command);

            // Assert
            Assert.NotEqual(default, sut.CreateDate);

            Assert.Equal(command.Make, sut.Make);
            Assert.Equal(command.Model, sut.Model);
            Assert.Equal(command.Colour, sut.Colour);
            Assert.Equal(command.Year, sut.Year);
        }

        [Fact]
        public void Change_ShouldUpdateProperties()
        {
            // Arrange
            var sut = fixture.Create<Car>();

            var command = fixture.Create<ChangeCarCommand>();

            // Act
            sut.Change(command);

            // Assert
            Assert.Equal(command.Make, sut.Make);
            Assert.Equal(command.Model, sut.Model);
            Assert.Equal(command.Colour, sut.Colour);
            Assert.Equal(command.Year, sut.Year);
        }
    }
}
