using System.Threading.Tasks;

namespace lphh_api.Service.Authentication;

public interface IAuthService
{
    Task<AuthResult> RegisterAsync(string email, string username, string password, string role);
    Task<AuthResult> RegisterUserAsync(string email, string username, string phoneNumber, string firstName,string lastName,string ward,string medicalNumber, string role, string identityId);
    Task<AuthResult> LoginAsync(string username, string password);
    Task<AuthRefreshRespond> RefreshAuth(string? authToken, string? refreshToken);

    Task<UserInfoModel> FindUserRole(string id);
}