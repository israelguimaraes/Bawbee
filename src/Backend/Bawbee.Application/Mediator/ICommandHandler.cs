using Bawbee.Application.Operations;
using MediatR;

namespace Bawbee.Application.Mediator
{
    public interface ICommandHandler<in TRequest> : IRequestHandler<TRequest, OperationResult> where TRequest : IRequest<OperationResult>
    {

    }
}
