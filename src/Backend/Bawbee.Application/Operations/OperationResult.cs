using MediatR;

namespace Bawbee.Application.Operations
{
    public abstract class OperationResult : IRequest<OperationResult>
    {
        public StatusResult Type { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
    }

    public class OkOperation : OperationResult
    {
        public OkOperation(object data = null)
        {
            Type = data == null ? StatusResult.OkWithoutReturn : StatusResult.Ok;
        }
    }

    public class InvalidOperation : OperationResult
    {
        public InvalidOperation(string message = "Invalid operation.")
        {
            Type = StatusResult.InvalidOperation;
            Message = message;
        }
    }

    public class ResultNotFoundOperation : OperationResult
    {
        public ResultNotFoundOperation(string message)
        {
            Type = StatusResult.NotFoundData;
            Message = message;
        }
    }
}
