using System.IdentityModel.Tokens.Jwt;

namespace Shared.Utils;

public static class TokenHelper
{
    public static bool IsTokenValid(string? authHeader, DateTime currentTime)
    {
        var token = $"{authHeader}".ToString().Replace("Bearer", "").Trim();

        JwtSecurityToken jwtSecurityToken;
        try
        {
            jwtSecurityToken = new JwtSecurityToken(token);
        }
        catch (Exception)
        {
            return false;
        }

        return jwtSecurityToken.ValidTo > currentTime.AddMinutes(10);
    }
}