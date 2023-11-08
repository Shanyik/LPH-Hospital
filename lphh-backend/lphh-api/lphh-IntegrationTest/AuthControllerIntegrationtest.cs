using System.Net;
using System.Net.Http.Headers;
using System.Text;
using lphh_api.Contracts;
using lphh_api.Service.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace lphh_IntegrationTest;

public class AuthControllerIntegrationtest : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;

    public AuthControllerIntegrationtest(CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }
    
    [Fact]
    public async Task Post_Login_OK()
    {
        // Arrange
        var client = _factory.CreateClient();
       

        //Act
        var authRequest = new AuthRequest("admin@admin.com", "admin123");
        
        var content = new StringContent(JsonConvert.SerializeObject(authRequest), Encoding.UTF8, "application/json");

        var response = await client.PostAsync("/api/Auth/Login", content);
        

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async Task Post_Register_OK()
    {
        // Arrange
        var client = _factory.CreateClient();
       

        //Act
        var regRequest = new RegistrationRequest("string1@string.com", "string", "string", "Patient", "+111111111", "asd", "asdf", "a", "123-456-788");
        
        var content = new StringContent(JsonConvert.SerializeObject(regRequest), Encoding.UTF8, "application/json");

        var response = await client.PostAsync("/api/Auth/Register", content);
        

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}