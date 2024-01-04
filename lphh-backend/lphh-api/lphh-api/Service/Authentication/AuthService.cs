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

        return new AuthResult(true, validInput.Email, validInput.UserName, validInput.Id, accessToken);
    }
    
    private static AuthResult InvalidEmailOrUsername(string input)
    {
        var result = new AuthResult(false, input, "", "", "");
        result.ErrorMessages.Add("Bad credentials", "Invalid email or username");
        return result;
    }

    private static AuthResult InvalidPassword(string input, string userName)
    {
        var result = new AuthResult(false, input, userName, "", "");
        result.ErrorMessages.Add("Bad credentials", "Invalid password");
        return result;
    }
}