using System.Linq;
using AutoFixture;
using Cars.Api.Extensions;
using Cars.Api.Settings;
using Microsoft.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;
using Xunit;

namespace Cars.Api.UnitTests.Container
{
    public class ContainerTests
    {
        private readonly Fixture fixture = new Fixture();

        [Fact]
        public void Verify_SuccessfulRegistrations()
        {
            // Arrange
            var applicationSettings = fixture.Create<ApplicationSettings>();

            var container = new SimpleInjector.Container();

            RegisterMvcControllers(container);

            container.RegisterComponents(applicationSettings);

            // Act
            // Assert
            container.Verify();
        }

        private static void RegisterMvcControllers(SimpleInjector.Container container)
        {
            var assembly = typeof(Startup).Assembly;
            var apiLifestyle = new AsyncScopedLifestyle();

            container.Options.DefaultScopedLifestyle = apiLifestyle;

            var controllerType = typeof(Controller);
            var controllerTypes = assembly.GetTypes().Where(t => t.IsSubclassOf(controllerType));

            foreach (var type in controllerTypes)
            {
                container.Register(type, type, apiLifestyle);
            }
        }
    }
}
