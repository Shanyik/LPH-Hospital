using lphh_api.Model;
using lphh_api.Repository.DoctorRepo;
using lphh_api.Repository.PatientRepo;
using Microsoft.AspNetCore.Identity;

namespace lphh_api.Service.Authentication;

public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IPatientRepository _patientRepository;
    private readonly IDoctorRepository _doctorRepository;

    public AuthService(UserManager<IdentityUser> userManager, ITokenService tokenService, IPatientRepository patientRepository, IDoctorRepository doctorRepository)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _doctorRepository = doctorRepository;
        _patientRepository = patientRepository;
    }

    public async Task<AuthResult> RegisterAsync(string email, string username, string password, string role)
    {
        var user = new IdentityUser { UserName = username, Email = email };
        var result = await _userManager.CreateAsync(user, password);
        Console.WriteLine(user.Id);

        if (!result.Succeeded)
        {
            return FailedRegistration(result, email, username);
        }

        await _userManager.AddToRoleAsync(user, role); // Adding the user to a role
        return new AuthResult(true, email, username, user.Id, "");
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
            
            await _doctorRepository.Add(newDoctor);
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
            
            await _patientRepository.Add(newPatient);
        }
        else
        {
            return new AuthResult(false, email, username, identityId, "");
        }
        
        return new AuthResult(true, email, username, identityId, "");
    }

    private static AuthResult FailedRegistration(IdentityResult result, string email, string username)
    {
        var authResult = new AuthResult(false, email, username, "", "");

        foreach (var error in result.Errors)
        {
            authResult.ErrorMessages.Add(error.Code, error.Description);
        }

        return authResult;
    }
    
    public async Task<AuthResult> LoginAsync(string email, string password)
    {
        var managedUser = await _userManager.FindByEmailAsync(email);

        if (managedUser == null)
        {
            return InvalidEmail(email);
        }

        var isPasswordValid = await _userManager.CheckPasswordAsync(managedUser, password);
        if (!isPasswordValid)
        {
            return InvalidPassword(email, managedUser.UserName);
        }


        // get the role and pass it to the TokenService
        var roles = await _userManager.GetRolesAsync(managedUser);
        var accessToken = _tokenService.CreateToken(managedUser, roles[0]);

        return new AuthResult(true, managedUser.Email, managedUser.UserName, managedUser.Id, accessToken);
    }

    private static AuthResult InvalidEmail(string email)
    {
        var result = new AuthResult(false, email, "", "", "");
        result.ErrorMessages.Add("Bad credentials", "Invalid email");
        return result;
    }

    private static AuthResult InvalidPassword(string email, string userName)
    {
        var result = new AuthResult(false, email, userName, "", "");
        result.ErrorMessages.Add("Bad credentials", "Invalid password");
        return result;
    }
}