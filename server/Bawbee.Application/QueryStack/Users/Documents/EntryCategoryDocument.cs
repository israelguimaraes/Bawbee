using System;

namespace Bawbee.Application.QueryStack.Users.Documents
{
    public class EntryCategoryDocument
    {
        public int EntryCategoryId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }

        public EntryCategoryDocument()
        {
            CreatedAt = DateTime.Now;
        }
    }
}