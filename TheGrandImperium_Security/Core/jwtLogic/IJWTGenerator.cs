using TheGrandImperium_Security.Core.Entities;

namespace TheGrandImperium_Security.Core.jwtLogic
{
    public interface IJWTGenerator
    {
        string CreateToken(Usuario usuario);
        string CreateRefreshToken();
    }
}
