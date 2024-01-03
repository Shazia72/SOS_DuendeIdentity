using IdentityModel.Client;
using Microsoft.Extensions.Options;
using System.Runtime.CompilerServices;

namespace MVC_App.Services
{
    public class TokenService : ITokenService
    {
        private readonly ILogger<TokenService> _logger;
        private IOptions<IdentityServerSettings> _identityServerSettings;
        private readonly DiscoveryDocumentResponse discoveryDocument;

        public TokenService( IOptions<IdentityServerSettings> identityServerSettings)
        {
            //_logger = logger;
            _identityServerSettings = identityServerSettings;

            using var httpClient = new HttpClient();
            discoveryDocument = httpClient.GetDiscoveryDocumentAsync(identityServerSettings.Value.DiscoveryUrl).Result;

            if (discoveryDocument.IsError)
            {
                //_logger.LogError($"Unable to get discovery document Error is {discoveryDocument.Error}");
                throw new Exception("unable to get discovery document");
            }

        }

       
        public async Task<TokenResponse> GetToken(string scope)
        {
            var client = new HttpClient();
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Scope = scope,
                Address = discoveryDocument.TokenEndpoint,
                ClientId = _identityServerSettings.Value.ClientName,
                ClientSecret = _identityServerSettings.Value.ClientPassword,
            });
            if (tokenResponse.IsError)
            {
                throw new Exception("unable to get token", tokenResponse.Exception);
            }
            return tokenResponse;
        }
    }
}
