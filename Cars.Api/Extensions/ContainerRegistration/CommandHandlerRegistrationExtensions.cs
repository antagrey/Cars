using Cars.Application.Command.Handlers;
using Cars.Application.Command.Results;
using Cars.Domain.Contracts;
using Cars.Infrastructure.Command;
using SimpleInjector;

namespace Cars.Api.Extensions.ContainerRegistration
{
    public static class CommandHandlerRegistrationExtensions
    {
        public static Container RegisterCommandHandlers(this Container container)
        {
            container.Register<IAsyncCommandHandler<AddCarCommand, CarAddedResult>, AddCarCommandHandler>(Lifestyle.Scoped);
            container.Register<IAsyncCommandHandler<RemoveCarCommand, CarRemovedResult>, RemoveCarCommandHandler>(Lifestyle.Scoped);
            container.Register<IAsyncCommandHandler<ChangeCarCommand, CarChangedResult>, ChangeCarCommandHandler>(Lifestyle.Scoped);

            return container;
        }
    }
}
