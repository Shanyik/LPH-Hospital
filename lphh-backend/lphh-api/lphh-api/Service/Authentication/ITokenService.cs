using lphh_api.Model;
using Microsoft.AspNetCore.Identity;

namespace lphh_api.Service.Authentication;

public interface ITokenService
{
    string CreateToken(IdentityUser user, string role);

    string GenerateRefreshToken();
}