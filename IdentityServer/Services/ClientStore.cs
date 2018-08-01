using IdentityServer3.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IdentityServer3.Core.Models;
using System.Threading.Tasks;
using IdentityServer3.Core;

namespace IdentityServer.Services
{
    public class ClientStore : IClientStore
    {
        List<Client> _clients = new List<Client>
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
                    PostLogoutRedirectUris = new List<string> {"https://localhost:44330/"},
                    AllowedScopes = new List<string> {
                        Constants.StandardScopes.OpenId,
                        Constants.StandardScopes.Profile,
                        Constants.StandardScopes.Email,
                        Constants.StandardScopes.Roles
                    },
                    AccessTokenType = AccessTokenType.Jwt
                }
            };

        public Task<Client> FindClientByIdAsync(string clientId)
        {
            IEnumerable<Client> query =
                from client in this._clients
                where client.ClientId == clientId && client.Enabled
                select client;
            return Task.FromResult<Client>(query.SingleOrDefault<Client>());
        }
    }
}