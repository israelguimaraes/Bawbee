using System;

namespace Bawbee.Core.Models
{
    public abstract class Document
    {
        public string Id { get; protected set; }
        public DateTime CreatedAt { get; set; }

        public Document()
        {
            CreatedAt = DateTime.Now;
        }
    }
}
