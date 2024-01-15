using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Castle.Core.Configuration;
using lphh_api.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace lphh_api.Service.Authentication;

public class TokenService : ITokenService
{
    private const int ExpirationMinutes = 20;
    
    private IConfiguration _configuration;

    public TokenService(IConfiguration configurationBinder)
    {
        _configuration = configurationBinder;
    }

    public string CreateToken(IdentityUser user, string role)
    {
        var expiration = DateTime.UtcNow.AddMinutes(ExpirationMinutes);
        var token = CreateJwtToken(
            CreateClaims(user, role),
            CreateSigningCredentials(),
            expiration
        );
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }
    
    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];

        using var generator = RandomNumberGenerator.Create();
        
        generator.GetBytes(randomNumber);

        return Convert.ToBase64String(randomNumber);
    }
    

    private JwtSecurityToken CreateJwtToken(List<Claim> claims, SigningCredentials credentials,
        DateTime expiration) =>
        new(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: expiration,
            signingCredentials: credentials
        );
    
    
    private List<Claim> CreateClaims(IdentityUser user, string? role)
    {
        try
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id)
            };

            if (role != null)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            
            return claims;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private SigningCredentials CreateSigningCredentials()
    {
        return new SigningCredentials(
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]) //user secret
            ),
            SecurityAlgorithms.HmacSha256
        );
    }
}