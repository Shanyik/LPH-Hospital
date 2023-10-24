using Microsoft.AspNetCore.Identity;

namespace lph_api.Service.Authentication;

public interface ITokenService
{
    string CreateToken(IdentityUser user, string role);
}