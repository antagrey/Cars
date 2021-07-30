using Cars.Api.Extensions.ContainerRegistration;
using Cars.Api.Settings;
using SimpleInjector;

namespace Cars.Api.Extensions
{
    public static class ContainerExtensions
    {
        public static Container RegisterComponents(this Container container, ApplicationSettings applicationSettings)
        {
            return container
                .RegisterFactories()
                .RegisterAdapters()
                .RegisterProviders(applicationSettings)
                .RegisterQueryHandlers()
                .RegisterCommandHandlers()
                .RegisterAclHandlers()
                .RegisterEntityFramework(applicationSettings);
        }
    }
}
