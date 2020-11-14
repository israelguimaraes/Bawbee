using MediatR;

namespace Bawbee.Domain.Core.Commands
{
    public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, CommandResult> where TCommand : IRequest<CommandResult>
    {
    }
}
