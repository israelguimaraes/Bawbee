using Bawbee.Infra.CrossCutting.Common.Security.Models;

namespace Bawbee.Infra.CrossCutting.Common.Security
{
    public interface IJwtService
    {
        UserAcessTokenDTO GenerateSecurityToken(int userId, string username, string email);
    }
}
