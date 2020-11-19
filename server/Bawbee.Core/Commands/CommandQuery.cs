using FluentValidation.Results;
using MediatR;

namespace Bawbee.Core.Commands
{
    public abstract class CommandQuery<T> : IRequest<T>
    {
        public ValidationResult ValidationResult { get; protected set; }

        public abstract bool IsValid();
    }
}
