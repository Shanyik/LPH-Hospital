using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace lphh_IntegrationTest;

public class GetALLEndpointIntegrationtest  : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory _factory;

    public GetALLEndpointIntegrationtest(CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }
    
    
    [Theory]
    [InlineData("/api/Doctor/GetAll")]
    [InlineData("/api/Event/GetAll")]
    [InlineData("/api/Patient/GetAll")]
    [InlineData("/api/Product/GetAll")]
    public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
    {
        // Arrange
        var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddAuthentication(defaultScheme: "Admin")
                        .AddScheme<AuthenticationSchemeOptions, FakeAuthenticationHandler>(
                            "Admin", options => { });
                });
            })
            .CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = true,
            });

        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(scheme: "Admin");
        
        // Act
        var response = await client.GetAsync(url);

        // Assert
        response.EnsureSuccessStatusCode(); 
        Assert.Equal("application/json; charset=utf-8", 
            response.Content.Headers.ContentType.ToString());
    }
}