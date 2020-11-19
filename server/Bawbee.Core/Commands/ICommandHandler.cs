using MediatR;

namespace Bawbee.Core.Commands
{
    public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, CommandResult> where TCommand : IRequest<CommandResult>
    {
    }
}
