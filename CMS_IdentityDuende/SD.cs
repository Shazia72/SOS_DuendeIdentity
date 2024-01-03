using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Validation;

namespace CMS_IdentityDuende
{
    public static class SD
    {
        public const string Admin = "admin";
        public const string Customer = "customer";
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
           new List<ApiScope>
           {
                //new ApiScope("magic", "Magic Server"),
                //new ApiScope(name: "read",   displayName: "Read your data."),
                //new ApiScope(name: "write",  displayName: "Write your data."),
                //new ApiScope(name: "delete", displayName: "Delete your data."),
               new ApiScope(name:"WebAPI.read"),
               new ApiScope(name:"WebAPI.write"),
           };
        public static IEnumerable<Client> Cleints =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "service.client",
                    ClientName = "Shazia",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "WebAPI.read", "WebAPI.write" }
                },
                 new Client
                {
                    ClientId = "interactive",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    //AllowedScopes = { "WebAPI.read", IdentityServerConstants.StandardScopes.OpenId
                    // ,IdentityServerConstants.StandardScopes.Profile,
                    // IdentityServerConstants.StandardScopes.Email},
                  
                    RedirectUris = {"https://localhost:5444/signin-oidc" },
                    FrontChannelLogoutUri = "https://localhost:5444/signout-oidc",
                    PostLogoutRedirectUris = { "https://localhost:5444/signout-callback-oidc" },
                    
                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid","profile","WebAPI.read" },
                    RequireConsent = true
                }
            };

        public static IEnumerable<ApiResource> apiResources =>
            new List<ApiResource>
            {
                new ApiResource
                {
                    Scopes = new List<string> {"WebAPI.read", "WebAPI.write"},
                    ApiSecrets = new List<Secret> {new Secret("secret".Sha256())},
                    UserClaims = new List<string> {"role"}
                }
            };
    }
}
