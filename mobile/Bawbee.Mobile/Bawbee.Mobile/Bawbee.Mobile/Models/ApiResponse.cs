using System.Collections.Generic;

namespace Bawbee.Mobile.Models
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public T Data { get; set; }
    }
}
