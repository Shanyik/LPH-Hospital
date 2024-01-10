namespace lphh_api.Service;

public class AuthRefreshRespond
{
    public AuthRefreshRespond(bool success, string authToken, string refreshToken)
    {
        Success = success;
        AuthToken = authToken;
        RefreshToken = refreshToken;
    }

    public bool Success { get; set; }
    
    public string AuthToken { get; set; }
    
    public string RefreshToken { get; set; }
}