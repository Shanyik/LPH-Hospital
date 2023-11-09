
using System.Net;
using System.Text;
using lphh_api.Context;
using lphh_api.Contracts;
using lphh_api.Service.Authentication;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Xunit.Abstractions;

namespace lphh_IntegrationTest;

public class AuthControllerIntegrationtest : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;
    
    private readonly ITestOutputHelper _output;  //Console not working in XUNIT test

    public AuthControllerIntegrationtest(CustomWebApplicationFactory factory, ITestOutputHelper output)
    {
        _factory = factory;
        _output = output;
    }
    
    
    [Fact]
    public async Task Auth_Test_Ok()
    {
        // Arrange
        var client = _factory.CreateClient();
        
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())  
            .AddJsonFile("appsettings.json")              
            .Build();
        
        //database setup
        var connectionString = configuration.GetConnectionString("Hospital");
        
        var dbContextOptions = new DbContextOptionsBuilder<HospitalApiContext>()
            .UseSqlServer(connectionString)
            .Options;
        
        var hospitalApiContext = new HospitalApiContext(dbContextOptions);

        //userManager setup
        var userManager = new UserManager<IdentityUser>(
            new UserStore<IdentityUser>(hospitalApiContext),
            null, null, null, null, null, null, null, null);
        
        //check to delet user before register again.
        IdentityResult deletionResult = null;
        
        var userToDelete = await userManager.FindByEmailAsync("testPatient@gamil.com");
        if (userToDelete != null)
        {
            deletionResult = await userManager.DeleteAsync(userToDelete);
            _output.WriteLine($"Deletion Result: {deletionResult.Succeeded}");
        }
        
        
        //Act
        // Act - Registration
        var regRequest = new RegistrationRequest("testPatient@gamil.com", "testPatient", "string", "Patient", "+111111111", "asd", "asdf", "a", "123-456-788");
        var content = new StringContent(JsonConvert.SerializeObject(regRequest), Encoding.UTF8, "application/json");
        var response = await client.PostAsync("/api/Auth/Register", content);
        var registrationResponse = JsonConvert.DeserializeObject<RegistrationResponse>(await response.Content.ReadAsStringAsync());
        
        // Act - Login
        var authRequest = new AuthRequest("testPatient@gamil.com", "string");
        var loginContent = new StringContent(JsonConvert.SerializeObject(authRequest), Encoding.UTF8, "application/json");
        var loginResponse = await client.PostAsync("/api/Auth/Login", loginContent);
        var loginResponseData = JsonConvert.DeserializeObject<AuthResponse>(await response.Content.ReadAsStringAsync());
        
        // Assert
        // Assert - Registration
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Equal(regRequest.Email, registrationResponse.Email);

        // Assert - Login
        Assert.Equal(HttpStatusCode.OK, loginResponse.StatusCode);
        Assert.Equal(authRequest.Email, loginResponseData.Email);
        
        // Assert - User Deleted
        Assert.True(deletionResult != null && deletionResult.Succeeded);
    }
    
    
    
}