using Bawbee.Application.Services.Secutity.Models;

namespace Bawbee.Infrastructure.Security.Jwt
{
    public interface ISecurityTokenService
    {
        UserAcessTokenDto GenerateToken(int userId, string username, string email);
    }
}
