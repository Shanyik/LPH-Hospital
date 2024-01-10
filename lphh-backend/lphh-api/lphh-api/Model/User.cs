using Microsoft.AspNetCore.Identity;

namespace lphh_api.Model;

public class User : IdentityUser
{
    public string? RefreshToken { get; set; }
    
    public DateTime RefreshTokenExpiry { get; set; }
}