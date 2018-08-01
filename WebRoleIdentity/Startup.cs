using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Extensions;
using IdentityServer3.Core.Resources;
using IdentityServer3.Core.Services;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebRoleIdentity.Services;

namespace WebRoleIdentity
{
    public class Startup
    {
        public static void Configuration(IAppBuilder app)
        {
            app.Map("/core",
                coreApp =>
                {
                    var factory = new IdentityServerServiceFactory
                    {
                        UserService = new Registration<IUserService>(new UserService()),
                        ClientStore = new Registration<IClientStore>(new ClientStore()),
                        ScopeStore = new Registration<IScopeStore>(new ScopeStore(Scopes.Get()))
                    };

                    coreApp.UseIdentityServer(new IdentityServerOptions
                    {
                        SiteName = "Standalone Identity Server",
                        SigningCertificate = Cert.Load(),
                        Factory = factory,
                        RequireSsl = true,
                        AuthenticationOptions = new IdentityServer3.Core.Configuration.AuthenticationOptions
                        {
                            EnablePostSignOutAutoRedirect = true
                        },
                        LoggingOptions = new LoggingOptions
                        {
                            EnableHttpLogging = true,
                            EnableKatanaLogging = true,
                            EnableWebApiDiagnostics = true,
                            WebApiDiagnosticsIsVerbose = true
                        },
                    });
                });
        }
    }
}