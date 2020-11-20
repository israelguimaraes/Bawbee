using System;

namespace Bawbee.Infra.Data.Documents
{
    public class EntryCategoryDocument
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }

        public EntryCategoryDocument()
        {
            CreatedAt = DateTime.Now;
        }
    }
}