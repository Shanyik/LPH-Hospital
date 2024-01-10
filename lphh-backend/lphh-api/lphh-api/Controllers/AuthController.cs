using System.Security.Claims;
using lphh_api.Contracts;
using lphh_api.Model;
using lphh_api.Service.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace lphh_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authenticationService;
    private readonly UserManager<User> _userManager;


    public AuthController(IAuthService authenticationService, UserManager<User> userManager)
    {
        _authenticationService = authenticationService;
        _userManager = userManager;
    }

    [HttpPost("Register")]
    public async Task<ActionResult<RegistrationResponse>> Register(RegistrationRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var identityRegistration = await _authenticationService.RegisterAsync(request.Email, request.Username, request.Password, request.Role);

        if (!identityRegistration.Success)
        {
            AddErrors(identityRegistration);
            return BadRequest(ModelState);
        }
        
        var userRegistration = await _authenticationService.RegisterUserAsync(request.Email, request.Username, request.PhoneNumber, request.FirstName, 
            request.LastName, request.Ward, request.MedicalNumber, request.Role,  identityRegistration.IdentityID);

        if (!userRegistration.Success)
        {
            AddErrors(userRegistration);
            return BadRequest(ModelState);
        }

        return CreatedAtAction(nameof(Register), new RegistrationResponse(userRegistration.Email, userRegistration.UserName));
    }

    private void AddErrors(AuthResult result)
    {
        foreach (var error in result.ErrorMessages)
        {
            ModelState.AddModelError(error.Key, error.Value);
        }
    }
    
    [HttpPost("Login")]
    public async Task<ActionResult<AuthResponse>> Authenticate([FromBody] AuthRequest request)
    {
        
        Console.WriteLine("fetch good");
        if (!ModelState.IsValid)
        {
            Console.WriteLine("not valid");
            return BadRequest(ModelState);
        }

        var result = await _authenticationService.LoginAsync(request.Email, request.Password);

        if (!result.Success)
        {
            Console.WriteLine("not success");
            AddErrors(result);
            return BadRequest(ModelState);
        }

        var refreshToken = result.RefreshToken;
        
        HttpContext.Response.Cookies.Append("access_token", result.Token); //http-only, secure 
        HttpContext.Response.Cookies.Append("refresh_token", refreshToken);
        return Ok(new AuthResponse(result.Email, result.UserName, result.Token));
    }
    
    [HttpPost("Logout")]
    public  async Task<IActionResult> Logout()
    {
        try
        {
            var userIdentifierClaim = HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userIdentifierClaim);
            if (user != null)
            {
                user.RefreshToken = null;
                user.RefreshTokenExpiry = DateTime.UtcNow;
                await _userManager.UpdateAsync(user);
                Response.Cookies.Delete("access_token");
                Response.Cookies.Delete("refresh_token");
                return Ok(); 
            }
            
            return BadRequest();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return BadRequest();
        }
        
    }

    [HttpPost("RefresToken")]
    public async Task<IActionResult> RefresToken()
    {
        try
        {
            var accessTokenCookie = HttpContext.Request.Cookies["access_token"];
            var refreshTokenCookie = HttpContext.Request.Cookies["refresh_token"];

            Console.WriteLine(accessTokenCookie);
            Console.WriteLine(refreshTokenCookie);

            var validationResult = await _authenticationService.RefreshAuth(accessTokenCookie, refreshTokenCookie);

            if (validationResult.Success)
            {
                HttpContext.Response.Cookies.Append("access_token", validationResult.AuthToken); //http-only, secure 
                HttpContext.Response.Cookies.Append("refresh_token", validationResult.RefreshToken);
                return Ok();
            }
            
            return Unauthorized();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    
    private void setTokenCookie(string token)
    {
        // append cookie with refresh token to the http response
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddDays(7)
        };
        Response.Cookies.Append("refreshToken", token, cookieOptions);
    }
}