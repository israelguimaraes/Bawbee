using FluentValidation.Results;
using System;

namespace Bawbee.Domain.Core.Commands
{
    public abstract class BaseCommand : Message
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        protected BaseCommand()
        {
            Timestamp = DateTime.Now;
        }

        public abstract bool IsValid();
    }
}
