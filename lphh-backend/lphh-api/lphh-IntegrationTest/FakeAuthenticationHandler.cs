using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace lphh_IntegrationTest;

public class FakeAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public FakeAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
        : base(options, logger, encoder, clock)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var identity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, "1"), 
            new Claim(ClaimTypes.Name, "TestUser"), 
            new Claim(ClaimTypes.Role, "Admin") 
        }, "fake");

        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, "fake");

        var result = AuthenticateResult.Success(ticket);

        return Task.FromResult(result);
    }
}