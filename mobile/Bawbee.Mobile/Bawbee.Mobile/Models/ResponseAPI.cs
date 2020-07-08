using System.Collections.Generic;

namespace Bawbee.Mobile.Models
{
    public class ResponseAPI<T>
    {
        public bool IsSuccess { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public T Data { get; set; }
    }
}
