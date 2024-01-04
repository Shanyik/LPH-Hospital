using lphh_api.Contracts;
using lphh_api.Service.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace lphh_api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authenticationService;

    public AuthController(IAuthService authenticationService)
    {
        _authenticationService = authenticationService;
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
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _authenticationService.LoginAsync(request.Email, request.Password);

        if (!result.Success)
        {
            AddErrors(result);
            return BadRequest(ModelState);
        }
        
        HttpContext.Response.Cookies.Append("access_token", result.Token);
        return Ok(new AuthResponse(result.Email, result.UserName, result.Token));
    }
    
    [HttpPost("Logout")]
    public IActionResult Logout()
    {
        Response.Cookies.Delete("access_token");
        
        return NoContent(); 
    }
}