using Bawbee.Application.Operations;
using MediatR;

namespace Bawbee.Application.Bus
{
    public abstract class BaseQuery : IRequest<OperationResult>
    {

    }
}
