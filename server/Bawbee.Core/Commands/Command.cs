using FluentValidation.Results;
using MediatR;
using System;

namespace Bawbee.Core.Commands
{
    public abstract class Command : Operation, IRequest// Message
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }

        public abstract bool IsValid();
    }
}
