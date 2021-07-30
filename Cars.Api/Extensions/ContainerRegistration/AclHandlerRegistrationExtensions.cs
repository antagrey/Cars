using Cars.Acl.DataMuse.Handlers;
using Cars.Acl.DataMuse.Queries;
using Cars.Acl.DataMuse.Results;
using Cars.Infrastructure.Query;
using SimpleInjector;

namespace Cars.Api.Extensions.ContainerRegistration
{
    public static class AclHandlerRegistrationExtensions
    {
        public static Container RegisterAclHandlers(this Container container)
        {
            container
                .Register<IAsyncQueryHandler<WordsSoundingLikeRequest, SoundsLikeWordResults>, GetWordsSoundingLikeHandler> (Lifestyle.Scoped);

            return container;
        }
    }
}
