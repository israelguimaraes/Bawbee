﻿using System.Collections.Generic;

namespace Bawbee.Mobile.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; }
        public T Data { get; set; }
    }
}
