using System;

namespace Bawbee.Infra.Data.Documents
{
    public class CategoryDocument
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }

        public CategoryDocument()
        {
            CreatedAt = DateTime.Now;
        }
    }
}