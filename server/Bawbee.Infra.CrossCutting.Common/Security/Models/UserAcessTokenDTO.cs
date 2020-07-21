using System;

namespace Bawbee.Infra.CrossCutting.Common.Security.Models
{
    public class UserAcessTokenDTO
    {
        public string AccessToken { get; set; }
        public DateTime ExpiresIn { get; set; }
    }
}
