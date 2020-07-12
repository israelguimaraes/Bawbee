using System;

namespace Bawbee.Mobile.Models.DTOs.Users
{
    public class UserAcessTokenDTO
    {
        public string AccessToken { get; set; }
        public DateTime ExpiresIn { get; set; }
    }
}
