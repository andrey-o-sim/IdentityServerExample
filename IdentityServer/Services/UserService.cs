using IdentityServer3.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IdentityServer3.Core.Models;
using System.Threading.Tasks;
using IdentityServer3.Core.Services.Default;
using IdentityServer3.Core.Services.InMemory;
using System.Security.Claims;
using IdentityServer3.Core;

namespace IdentityServer.Services
{
    public class UserService : UserServiceBase, IUserService
    {
        List<InMemoryUser> _users = new List<InMemoryUser>
            {
                new InMemoryUser
                {
                    Subject = "1",
                    Username = "Scott Brady",
                    Password = "Password123!",
                    Claims = new List<Claim> {
                        new Claim(Constants.ClaimTypes.GivenName, "Scott"),
                        new Claim(Constants.ClaimTypes.FamilyName, "Brady"),
                        new Claim(Constants.ClaimTypes.Email, "info@scottbrady91.com")
                    }
                },
                new InMemoryUser
                {
                    Subject = "2",
                    Username = "Backup",
                    Password = "Password123!",
                    Claims = new List<Claim> {
                        new Claim(Constants.ClaimTypes.GivenName, "Andrey"),
                        new Claim(Constants.ClaimTypes.FamilyName, "Simonenko"),
                        new Claim(Constants.ClaimTypes.Email, "backup@mail.com"),
                        new Claim(Constants.ClaimTypes.Role, "Admin")
                    }
                }
        };

        public override Task AuthenticateLocalAsync(LocalAuthenticationContext context)
        {
            var user = _users.Where(us => us.Username == context.UserName && us.Password == context.Password).FirstOrDefault();

            if (user != null)
            {
                context.AuthenticateResult = new AuthenticateResult(user.Subject.ToString(), user.Username, user.Claims);
            }

            return Task.FromResult(0);
            //return base.AuthenticateLocalAsync(context);
        }
    }
}