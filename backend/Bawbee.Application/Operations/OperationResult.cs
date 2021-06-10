using MediatR;

namespace Bawbee.Application.Operations
{
    public abstract class OperationResult : IRequest<OperationResult>
    {
        protected TypeResultEnum Type { get; set; }
        protected object Data { get; set; }
        protected string Message { get; set; }
        public abstract bool IsValid { get; }
    }

    public class OkOperation : OperationResult
    {
        public OkOperation(object data = null)
        {
            Type = data == null ? TypeResultEnum.OkWithoutDataResponse : TypeResultEnum.Ok;
        }

        public override bool IsValid => true;
    }

    public class InvalidOperation : OperationResult
    {
        public InvalidOperation(string message = "Invalid operation.")
        {
            Type = TypeResultEnum.InvalidOperation;
            Message = message;
        }

        public override bool IsValid => false;
    }
}
