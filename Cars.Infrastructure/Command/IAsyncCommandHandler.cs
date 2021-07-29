using System.Threading.Tasks;

namespace Cars.Infrastructure.Command
{
    public interface IAsyncCommandHandler<in TCommand, out TResult>
        where TCommand : ICommand
        where TResult : CommandResult
    {
        TResult Result { get; }

        Task HandleAsync(TCommand command);
    }
}
