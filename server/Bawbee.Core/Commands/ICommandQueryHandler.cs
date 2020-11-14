using MediatR;

namespace Bawbee.Domain.Core.Commands
{
    public interface ICommandQueryHandler<TCommand, TResult> : IRequestHandler<TCommand, TResult> where TCommand : IRequest<TResult>
    {

    }
}
