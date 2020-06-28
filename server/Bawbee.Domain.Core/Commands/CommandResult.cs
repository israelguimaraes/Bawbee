namespace Bawbee.Domain.Core.Commands
{
    public class CommandResult
    {
        public bool IsSuccess { get; private set; }
        public string Message { get; private set; }
        public object Data { get; private set; }

        private CommandResult() { }

        public static CommandResult Ok(object data = null)
        {
            return new CommandResult
            {
                IsSuccess = true,
                Data = data
            };
        }

        public static CommandResult Ok(string message, object data = null)
        {
            return new CommandResult
            {
                IsSuccess = true,
                Message = message,
                Data = data
            };
        }

        public static CommandResult Error(string message = null, object data = null)
        {
            return new CommandResult
            {
                IsSuccess = false,
                Message = message,
                Data = data
            };
        }
    }
}
