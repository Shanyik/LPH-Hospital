﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using lphh_api.Model;
using lphh_api.Repository.AdminRepo;
using lphh_api.Repository.DoctorRepo;
using lphh_api.Repository.PatientRepo;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace lphh_api.Service.Authentication;

public class AuthService : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IPatientRepository _patientRepository;
    private readonly IDoctorRepository _doctorRepository;
    private readonly IAdminRepository _adminRepositroy;
    private IConfiguration _configuration;
    private readonly SignInManager<User> _sginManager;

    public AuthService(UserManager<User> userManager, ITokenService tokenService, IPatientRepository patientRepository, IDoctorRepository doctorRepository, IConfiguration configuration, IAdminRepository adminRepositroy, SignInManager<User> sginManager)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _doctorRepository = doctorRepository;
        _configuration = configuration;
        _adminRepositroy = adminRepositroy;
        _sginManager = sginManager;
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

    public async Task<UserInfoModel> FindUserRole(string id)
    {
        var patient = await _patientRepository.GetByIdentityId(id);
        var doctor = await _doctorRepository.GetByIdentityId(id);
        var admin = await _adminRepositroy.GetByIdentityId(id);

        
        
        if (patient != null)
        {
            return new UserInfoModel("Patient", patient.Id.ToString());
        }
        else if (doctor != null)
        {
            return new UserInfoModel("Doctor", doctor.Id.ToString());
        }
        else if (admin != null)
        {
            return new UserInfoModel("Admin", admin.Id.ToString());
        }

        return new UserInfoModel("", "");
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
        //var isPasswordValid = await _userManager.CheckPasswordAsync(validInput, password);
        var test = await _sginManager.PasswordSignInAsync(validInput, password, isPersistent: false, lockoutOnFailure: true);

        Console.WriteLine(test.Succeeded);
        if (!test.Succeeded)
        {
            return InvalidPassword(input, validInput.UserName);
        }
       
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
        
        if (principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value is null)
            return InvalidAuthTokenOrAttempt();

        var user = await _userManager.FindByIdAsync(principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        
        
        if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiry < DateTime.UtcNow)
            return InvalidAuthTokenOrAttempt();
        
        var userRole = "";
        
        var checkDoctor = await _doctorRepository.GetByIdentityId(user.Id);
        var checkPatient = await _patientRepository.GetByIdentityId(user.Id);
        var checkAdmin = await _adminRepositroy.GetByIdentityId(user.Id);

        if (checkDoctor != null)
        {
            userRole = "Doctor";
        }
        else if (checkPatient != null)
        {
            userRole = "Patient";
        }
        else if (checkAdmin != null)
        {
            userRole = "Admin";
        }

        
        if (userRole == "")
            return InvalidAuthTokenOrAttempt();
        
        var newAuthToken = _tokenService.CreateToken(user, userRole);
        var newRefreshToken = _tokenService.GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
        await _userManager.UpdateAsync(user);

        return new AuthRefreshRespond(true, newAuthToken, newRefreshToken);

    }
    
    private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token) 
    {
        try
        {
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            Console.WriteLine(key);

            var validation = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            
            var res = new JwtSecurityTokenHandler().ValidateToken(token, validation, out _ );
            Console.WriteLine(res);
            return res;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return null;
        }
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