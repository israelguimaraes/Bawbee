using Bawbee.Application.Operations;
using FluentValidation.Results;
using MediatR;
using System;

namespace Bawbee.Application.Bus
{
    public abstract class BaseCommand : IRequest<OperationResult>
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; protected set; }

        protected BaseCommand()
        {
            Timestamp = DateTime.Now;
        }

        public abstract bool IsValid();
    }
}
