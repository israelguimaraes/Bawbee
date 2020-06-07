namespace Bawbee.Infra.CrossCutting.Common.Security.Models
{
    public class UserTokenDTO
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
