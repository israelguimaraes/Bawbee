using MediatR;

namespace Bawbee.Core.Commands
{
    public interface ICommandQueryHandler<TCommand, TResult> : IRequestHandler<TCommand, TResult> where TCommand : IRequest<TResult>
    {

    }
}
