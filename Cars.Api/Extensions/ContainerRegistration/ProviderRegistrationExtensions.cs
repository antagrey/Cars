using Cars.Api.Settings;
using Cars.Application.Providers;
using SimpleInjector;

namespace Cars.Api.Extensions.ContainerRegistration
{
    public static class ProviderRegistrationExtensions
    {
        public static Container RegisterProviders(this Container container, ApplicationSettings applicationSettings)
        {
            container.RegisterSingleton<IConnectionStringsProvider>(() => applicationSettings.ConnectionStringSettings);
            container.RegisterSingleton<IUrlsProvider>(() => applicationSettings.CarUrlSettings);

            return container;
        }
    }
}
