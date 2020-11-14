using System;

namespace Bawbee.Core.Domain
{
    public abstract class Document
    {
        public string Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        public Document()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
