using Cars.Application.Query.Handlers;
using Cars.Application.Query.Queries;
using Cars.Application.Query.Results;
using Cars.Infrastructure.Query;
using SimpleInjector;

namespace Cars.Api.Extensions.ContainerRegistration
{
    public static class QueryHandlerRegistrationExtensions
    {
        public static Container RegisterQueryHandlers(this Container container)
        {
            container
                .Register<IAsyncQueryHandler<GetCarByIdQuery, CarResult>, GetCarByIdQueryHandler>(Lifestyle.Scoped);

            return container;
        }
    }
}
