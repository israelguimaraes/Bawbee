using System;

namespace Bawbee.Application.Query.Base
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
