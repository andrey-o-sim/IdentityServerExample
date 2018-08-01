using IdentityServer.Services;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Extensions;
using IdentityServer3.Core.Services;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace IdentityServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.Map("/core",
                coreApp =>
                {
                    var factory = new IdentityServerServiceFactory();
                    factory.UserService = new Registration<IUserService>(new UserService());
                    factory.ClientStore = new Registration<IClientStore>(new ClientStore());
                    factory.ScopeStore = new Registration<IScopeStore>(new ScopeStore(Scopes.Get()));

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