using System;

namespace Bawbee.Application.Services.Secutity.Models
{
    public class UserAcessTokenDto
    {
        public string AccessToken { get; set; }
        public DateTime ExpiresIn { get; set; }
    }
}
