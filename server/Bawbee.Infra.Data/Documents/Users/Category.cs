using System;

namespace Bawbee.Infra.Data.Documents
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }

        public Category()
        {
            CreatedAt = DateTime.Now;
        }
    }
}