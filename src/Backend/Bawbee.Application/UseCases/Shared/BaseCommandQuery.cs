using Bawbee.Application.Operations;

namespace Bawbee.Application.UseCases.Shared
{
    public abstract class BaseCommandQuery
    {
        protected OperationResult Ok(object data)
        {
            return new OkOperation(data);
        }

        protected OperationResult NotFound(string message)
        {
            return new ResultNotFoundOperation(message);
        }
    }
}
