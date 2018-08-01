using IdentityServer3.Core;
using IdentityServer3.Core.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "implicitclient",
                    ClientName = "Example Implicit Client",
                    Enabled = true,
                    Flow = Flows.Implicit,
                    RequireConsent = true,
                    AllowRememberConsent = true,
                    RedirectUris = new List<string> {"https://localhost:44345/authorization/signInCallback"},
                    PostLogoutRedirectUris = new List<string> {"https://localhost:44345/"},
                    /*AllowedScopes = new List<string> {
                        Constants.StandardScopes.OpenId,
                        Constants.StandardScopes.Profile,
                        Constants.StandardScopes.Email,
                        Constants.StandardScopes.Roles
                    },*/
                    AllowAccessToAllScopes = true
                    //AccessTokenType = AccessTokenType.Jwt
                }
            };
        }
    }
}