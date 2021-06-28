using MediatR;

namespace Bawbee.Application.Operations
{
    public abstract class OperationResult : IRequest<OperationResult>
    {
        public Status Status { get; set; }
        public object Data { get; set; }
        public string Message { get; set; }
    }

    public class OkOperation : OperationResult
    {
        public OkOperation(object data = null)
        {
            Status = data == null ? Status.OkWithoutReturn : Status.Ok;
            Data = data;
        }
    }

    public class InvalidOperation : OperationResult
    {
        public InvalidOperation(string message = "Invalid operation.")
        {
            Status = Status.InvalidOperation;
            Message = message;
        }
    }

    public class ResultNotFoundOperation : OperationResult
    {
        public ResultNotFoundOperation(string message)
        {
            Status = Status.NotFoundData;
            Message = message;
        }
    }
}
