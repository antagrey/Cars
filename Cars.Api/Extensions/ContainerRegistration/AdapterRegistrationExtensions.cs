using Cars.Infrastructure.Adapters;
using SimpleInjector;

namespace Cars.Api.Extensions.ContainerRegistration
{
    public static class AdapterRegistrationExtensions
    {
        public static Container RegisterAdapters(this Container container)
        {
            container.Register<IHttpClientAdapter, HttpClientAdapter>(Lifestyle.Scoped);

            return container;
        }
    }
}
