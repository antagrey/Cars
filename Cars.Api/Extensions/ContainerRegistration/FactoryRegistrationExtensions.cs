using Cars.Infrastructure.DataAccess;
using SimpleInjector;

namespace Cars.Api.Extensions.ContainerRegistration
{
    public static class FactoryRegistrationExtensions
    {
        public static Container RegisterFactories(this Container container)
        {
            container.RegisterSingleton<IDapperConnectionFactory, DapperConnectionFactory>();

            return container;
        }
    }
}
