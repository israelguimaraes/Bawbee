using Bawbee.Application.Operations;
using MediatR;

namespace Bawbee.Application.Mediator
{
    public abstract class BaseQuery : IRequest<OperationResult>
    {

    }
}
