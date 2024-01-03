using IdentityModel.Client;

namespace MVC_App.Services
{
    public interface ITokenService
    {
        Task<TokenResponse> GetToken(string scope);
    }
}
