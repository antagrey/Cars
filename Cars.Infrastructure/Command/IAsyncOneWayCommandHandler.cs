using System.Threading.Tasks;

namespace Cars.Infrastructure.Command
{
    public interface IAsyncOneWayCommandHandler<in TCommand> where TCommand : ICommand
    {
        Task HandleAsync(TCommand command);
    }
}
