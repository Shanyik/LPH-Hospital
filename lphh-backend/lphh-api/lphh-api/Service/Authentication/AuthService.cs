using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using lphh_api.Model;
using lphh_api.Repository.AdminRepo;
using lphh_api.Repository.DoctorRepo;
using lphh_api.Repository.PatientRepo;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace lphh_api.Service.Authentication;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IPatientRepository _patientRepository;
    private readonly IDoctorRepository _doctorRepository;
    private readonly IAdminRepository _adminRepositroy;
    private IConfiguration _configuration;

    public AuthService(UserManager<User> userManager, ITokenService tokenService, IPatientRepository patientRepository, IDoctorRepository doctorRepository, IConfiguration configuration, IAdminRepository adminRepositroy)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _doctorRepository = doctorRepository;
        _configuration = configuration;
        _adminRepositroy = adminRepositroy;
        _patientRepository = patientRepository;
    }

    public async Task<AuthResult> RegisterAsync(string email, string username, string password, string role)
    {
        var user = new User { UserName = username, Email = email };
        var result = await _userManager.CreateAsync(user, password);
        Console.WriteLine(user.Id);

        if (!result.Succeeded)
        {
            return FailedRegistration(result, email, username);
        }

        await _userManager.AddToRoleAsync(user, role); // Adding the user to a role
        return new AuthResult(true, email, username, user.Id, "", "");
    }
    
    public async Task<AuthResult> RegisterUserAsync(string email, string username, string phoneNumber, string firstName,string lastName,string ward,string medicalNumber, string role, string identityId)
    {

        if (role == "Doctor")
        {
            var newDoctor = new Doctor
            {
                Username = username,
                Email = email,
                PhoneNumber = phoneNumber,
                FirstName = firstName,
                LastName = lastName,
                Ward = ward,
                IdentityId = identityId
            }; 
            
             _doctorRepository.Add(newDoctor);
             

        }
        else if (role == "Patient")
        {
            var newPatient = new Patient
            {
                Username = username,
                Email = email,
                PhoneNumber = phoneNumber,
                FirstName = firstName,
                LastName = lastName,
                MedicalNumber = medicalNumber,
                IdentityId = identityId
            };
            

             _patientRepository.Add(newPatient);
             
        }
        else
        {
            return new AuthResult(false, email, username, identityId, "", "");
        }
        
        return new AuthResult(true, email, username, identityId, "", "");
    }

    private static AuthResult FailedRegistration(IdentityResult result, string email, string username)
    {
        var authResult = new AuthResult(false, email, username, "", "", "");

        foreach (var error in result.Errors)
        {
            authResult.ErrorMessages.Add(error.Code, error.Description);
        }

        return authResult;
    }
    
    public async Task<AuthResult> LoginAsync(string input, string password)
    {
        var managedUserByEmail = await _userManager.FindByEmailAsync(input);

        var managedUserByUserName = await _userManager.FindByNameAsync(input);

        if (managedUserByEmail == null && managedUserByUserName == null)
        {
            return InvalidEmailOrUsername(input);
        }

        var validInput = managedUserByEmail != null ? managedUserByEmail : managedUserByUserName;
        
        var isPasswordValid = await _userManager.CheckPasswordAsync(validInput, password);
        if (!isPasswordValid)
        {
            return InvalidPassword(input, validInput.UserName);
        }


        // get the role and pass it to the TokenService
        var roles = await _userManager.GetRolesAsync(validInput);
        var accessToken = _tokenService.CreateToken(validInput, roles[0]);
        var refreshToken = _tokenService.GenerateRefreshToken();

        validInput.RefreshToken = refreshToken;
        validInput.RefreshTokenExpiry = DateTime.Now.AddDays(7);
        await _userManager.UpdateAsync(validInput);
        
        return new AuthResult(true, validInput.Email, validInput.UserName, validInput.Id, accessToken, refreshToken);
    }

    public async Task<AuthRefreshRespond> RefreshAuth(string? authToken, string? refreshToken)
    {
        var principal = GetPrincipalFromExpiredToken(authToken);

        if (principal?.Identity?.Name is null)
            return InvalidAuthTokenOrAttempt();

        var user = await _userManager.FindByIdAsync(principal?.Identity?.Name);

        if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiry < DateTime.UtcNow)
            return InvalidAuthTokenOrAttempt();

        var userRole = "";
        
        var checkDoctor = await _doctorRepository.GetByIdentityId(user.Id);
        var checkPatient = await _patientRepository.GetByIdentityId(user.Id);
        var checkAdmin = await _adminRepositroy.GetByIdentityId(user.Id);

        if (checkDoctor != null)
        {
            userRole = "doctor";
        }
        else if (checkPatient != null)
        {
            userRole = "patient";
        }
        else if (checkAdmin != null)
        {
            userRole = "admin";
        }

        if (userRole == "")
            return InvalidAuthTokenOrAttempt();
        
        var newAuthToken = _tokenService.CreateToken(user, userRole);
        var newRefreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow;
        await _userManager.UpdateAsync(user);

        return new AuthRefreshRespond(true, newAuthToken, newRefreshToken);

    }
    
    private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
    {
        var Key = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);

        var validation = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Key)
        };

        return new JwtSecurityTokenHandler().ValidateToken(token, validation, out _);
    }
    
    private static AuthResult InvalidEmailOrUsername(string input)
    {
        var result = new AuthResult(false, input, "", "", "", "");
        result.ErrorMessages.Add("Bad credentials", "Invalid email or username");
        return result;
    }

    private static AuthResult InvalidPassword(string input, string userName)
    {
        var result = new AuthResult(false, input, userName, "", "", "");
        result.ErrorMessages.Add("Bad credentials", "Invalid password");
        return result;
    }

    private static AuthRefreshRespond InvalidAuthTokenOrAttempt()
    {
        var result = new AuthRefreshRespond(false, "", "");
        return result;
    }
}