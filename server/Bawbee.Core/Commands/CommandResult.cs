using System.Collections.Generic;
using System.Linq;

namespace Bawbee.Core.Commands
{
    public class CommandResult
    {
        public bool Success { get; private set; }
        public ICollection<string> Errors { get; private set; }
        public object Data { get; private set; }

        private CommandResult() { }

        public static CommandResult Ok(object data = null)
        {
            return new CommandResult
            {
                Success = true,
                Data = data
            };
        }

        public static CommandResult Ok(string message, object data = null)
        {
            return new CommandResult
            {
                Success = true,
                Errors = new List<string> { message },
                Data = data
            };
        }

        public static CommandResult Error(IEnumerable<string> errors = null)
        {
            return new CommandResult
            {
                Success = false,
                Errors = errors?.ToList(),
            };
        }
    }
}
