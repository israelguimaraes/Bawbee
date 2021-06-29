using Bawbee.Application.Operations;
using MediatR;

namespace Bawbee.Application.Bus
{
    public interface ICommandHandler<in TRequest> : IRequestHandler<TRequest, OperationResult> where TRequest : IRequest<OperationResult>
    {

    }
}
